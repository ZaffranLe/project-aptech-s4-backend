<!--  
Stateless Designer statemachine.
For more information: http://statelessdesigner.codeplex.com/
-->
<statemachine xmlns="http://statelessdesigner.codeplex.com/Schema">
  <settings>
    <itemname>Flow_Update%EntityName%Request</itemname>
    <namespace>QuantEdge.Worker.RiskManager.Processor.Update%EntityName%Request</namespace>
    <class>public</class>
  </settings>
  <triggers>
    <trigger>Next</trigger>
    <trigger>Error</trigger>
  </triggers>
  <states>
    <state start="yes">Input</state>
    <state>CheckValidate</state>
    <state>Process</state>
    <state>Output</state>
  </states>
  <transitions>
    <transition trigger="Next" from="Input" to="CheckValidate" />
    <transition trigger="Next" from="CheckValidate" to="Process" />
    <transition trigger="Next" from="Process" to="Output" />
    <transition trigger="Error" from="CheckValidate" to="Output" />
  </transitions>



</statemachine>