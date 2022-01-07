# Log4View.Extensions
Sample extensions for Log4View V2.

Please use this extensions as a blueprint for your own custom extensions for Log4View V2.

**SampleReceiver**
A custom Log4View receiver named *ContosoReceiver*. 
This is a file receiver which uses an own message parser *ContosoParser* to parse a custom log file format.
A sample log file using this file format is provided in *'ContosoLog.txt'*.
The WPF user control *ContosoReceiverConfigControl* shows how to create the configuration UI for this receiver.

**SampleView*
A simple custom view to demonstrate the possibility of creating own visualizations as shown in *'SampleViewControl.xaml'*.

