<!--  
Stateless Designer statemachine.
For more information: http://statelessdesigner.codeplex.com/
-->
<statemachine xmlns="http://statelessdesigner.codeplex.com/Schema">
  <settings>
    <itemname>Flow_GetList%EntityName%Request</itemname>
    <namespace>QuantEdge.Worker.DataProviderManager.Processor.GetList%EntityName%Request</namespace>
    <class>public</class>
  </settings>
 <triggers>     
    <trigger>Next</trigger>
  </triggers>
  <states>     
    	<state start="yes">ListRequest</state>
	<state>ProcessList</state>
	<state>ListResponse</state>	
  </states>
  <transitions>
  	<transition trigger="Next" from="ListRequest" to="ProcessList" />
	<transition trigger="Next" from="ProcessList" to="ListResponse" />
  </transitions>
</statemachine>