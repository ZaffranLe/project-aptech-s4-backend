﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Antlr.Runtime.Misc;
using ElectricShop.Common.Config;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.ReaderDatabase;
using Newtonsoft.Json;

namespace ElectricShop
{
    public class AppGlobal
    {
        private const string EXTENSION_FILE_NAME = ".qes"; // quant edge setting
        private const string DEFAULT_FOLDER_CONFIG = @"\Config\";
        public static ElectricConfig ElectricConfig { get; set; }
        public static bool InitMemory()
        {
            try
            {
                var lstEntityName = Memory.Memory.GetListEntityNameInit();
                List<EntityQuery> lstEntityQuery = lstEntityName.Select(x => EntityQuery.RequestAllByName(x)).ToList();
                lstEntityQuery = ProcessReadDatabaseUtils.GetListEntityQuery(lstEntityQuery);
                foreach (var entityQuery in lstEntityQuery)
                {
                    if (entityQuery.ReturnValue != null)
                    {
                        foreach (var entity in entityQuery.ReturnValue)
                        {

                            MemorySet.UpdateAndInsertEntity(entity.GetEntity());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool InitUserPermission(string reason)
        {
            try
            {
                // init bang memory
                // lay tat ca user cua he thong
                var lstUserInfo = MemoryInfo.GetAllUserInfo();
                foreach (var userInfo in lstUserInfo)
                {
                    // check xem user thuoc nhom quyen nao
                    var lstUserRole =
                        MemoryInfo.GetListUserRoleByField(userInfo.IdUserLogin.ToString(), UserRole.UserRoleFields.IdUserLogin);
                    if (lstUserRole.Count == 0)
                        continue;//chua dc gan quyen
                    var userRole = lstUserRole.FirstOrDefault(x => x.IdUserLogin == userInfo.IdUserLogin);
                    if (userRole == null)
                        continue; // chua duoc gan quyen
                    var lstRolePermission = MemoryInfo.GetListRolePermissionByField(userRole.IdRole.ToString(), RolePermission.RolePermissionFields.IdRole);
                    if (lstRolePermission.Count == 0)
                        continue; // nhom quyen chua duoc them permission
                    Dictionary<int, int> dicPermissionRole = new Dictionary<int, int>();
                    foreach (var rolePermission in lstRolePermission)
                    {
                        dicPermissionRole[rolePermission.IdPermission] = 1;
                    }
                    var lstPermission = MemoryInfo.GetAllPermission().Where(x => dicPermissionRole.ContainsKey(x.Id)).Select(x => x.Name).ToList();
                    Memory.Memory.DicUserPermission[userInfo.IdUserLogin] = new List<string>();
                    if (lstPermission.Count == 0)
                        continue; // khong co permission nao ca
                    Memory.Memory.DicUserPermission[userInfo.IdUserLogin].AddRange(lstPermission);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("Init Permission voi resson:{0} that bai", reason));
                Logger.Write(ex.ToString(),true);
                return false;
            }
            return true;
        }

        public static void ReloadUserPermission(int userLoginId)
        {
            try
            {
                // check xem user thuoc nhom quyen nao
                var lstUserRole =
                    MemoryInfo.GetListUserRoleByField(userLoginId.ToString(), UserRole.UserRoleFields.IdUserLogin);
                if (lstUserRole.Count == 0)
                    return ;//chua dc gan quyen
                var userRole = lstUserRole.FirstOrDefault(x => x.IdUserLogin == userLoginId);
                if (userRole == null)
                    return; // chua duoc gan quyen
                var lstRolePermission = MemoryInfo.GetListRolePermissionByField(userRole.IdRole.ToString(), RolePermission.RolePermissionFields.IdRole);
                if (lstRolePermission.Count == 0)
                    return; // nhom quyen chua duoc them permission
                Dictionary<int, int> dicPermissionRole = new Dictionary<int, int>();
                foreach (var rolePermission in lstRolePermission)
                {
                    dicPermissionRole[rolePermission.IdPermission] = 1;
                }
                var lstPermission = MemoryInfo.GetAllPermission().Where(x => dicPermissionRole.ContainsKey(x.Id)).Select(x => x.Name).ToList();
                Memory.Memory.DicUserPermission[userLoginId] = new List<string>();
                if (lstPermission.Count == 0)
                    return; // khong co permission nao ca
                Memory.Memory.DicUserPermission[userLoginId].AddRange(lstPermission);

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("Reload Permission voi userId:{0} that bai", userLoginId));
                Logger.Write(ex.ToString(),true);
            }
        }

        public static bool InitConfig()
        {
            try
            {
                #region Lay WebapiCnf
                var path = GetAppPath() + DEFAULT_FOLDER_CONFIG + ElectricConfig.FileName() + EXTENSION_FILE_NAME;
                if (!File.Exists(path))
                {
                    var strMsg = "Not found file in path:" + path;
                    throw new Exception(strMsg);
                }
                var fullText = File.ReadAllText(path);
                ElectricConfig = JsonConvert.DeserializeObject<ElectricConfig>(fullText);
                if (ElectricConfig == null)
                {
                    Logger.Write("Not get ElectricConfig");
                    Process.GetCurrentProcess().Kill();
                    return false;
                }
                Logger.Write("Get config CashTranferWebAPi success!");
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(),true);
            }
            return false;
        }
        public static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = System.AppDomain.CurrentDomain.BaseDirectory; //Dung iss phai lay local path qua func nay
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }
    }
}