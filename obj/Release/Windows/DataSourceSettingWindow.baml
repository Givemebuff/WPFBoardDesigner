   M S B A M L   `   `   `  ���� H  DBoardDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:BoardDesigner.WindowsBoardDesigner.Windows  3    -BoardDesigner.Windows.DataSourceSettingWindow   s oInfragisticsWPF4.DataPresenter.v14.2, Version=14.2.20142.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbe aInfragisticsWPF4.v14.2, Version=14.2.20142.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbl hInfragisticsWPF4.Ribbon.v14.2, Version=14.2.20142.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbm iInfragisticsWPF4.Editors.v14.2, Version=14.2.20142.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbq mInfragisticsWPF4.DockManager.v14.2, Version=14.2.20142.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb<igWPF(http://schemas.infragistics.com/xaml/wpf      9windows#clr-namespace:BoardDesigner.Windows      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	      � Title$  设置数据源��$	��600��$	��800�� � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterOwner=�.���  � RowDefinitions �� �� $��50 �.+�1  +Infragistics.Windows.Controls.XamTabControl  -     Name$ MainTab��   ThemeG  AInfragistics.Windows.Themes.Internal.PrimitivesThemeTypeConverter$ 
Office2013  �� TabStripPlacement$
 Left=�.@�-  'Infragistics.Windows.Controls.TabItemEx  $��	数据库��.���  � ColumnDefinitions �� $	��100 ��� $��* �.+��� -    ��Name$ DSList��$��0��	 �� DisplayMemberPath$
	 Name������ *DataBaseDataSourceList+
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType3   -BoardDesigner.Windows.DataSourceSettingWindow"  �� $��3��� $��1�� �� �� $��2* ��� $��40 �.+��  �� �� ��  �� $
��Auto ��� $��3* ��� $
��Auto ��� $��3* �.+��� $��	名称：��$��Center=�$��Center=�$��Center=�$��Center=��� $��	地址：��$��2��$��Center=�$��Center=�$��Center=�$��Center=��� $��用户名：��$��1��$��Center=�$��Center=�$��Center=�$��Center=��� $��	密码：��$��2��$��1��$��Center=�$��Center=�$��Center=�$��Center=��� -    ��Name$ DSNameTB��$��1��$��0��$��15q����� 
 ��  �� Mode$ FindAncestor=�"   �� Path$ SelectedItem.Name��� -   $ DSIPTB��$��3��$��0��$��15q����� 
 �� $ FindAncestor=�"  $ SelectedItem.Address��� -   $ DSUNTB��$��1��$��1��$��15q����� 
 �� $ FindAncestor=�"  $ SelectedItem.UserName��� -   $ DSPWB��$��3��$��1��$��15q����� 
 �� $ FindAncestor=�"  $ SelectedItem.PassWord��� $��1��$��2�� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ DSDBCB��$
��Left=�$��
15,15,0,15q� �� 
IsEditable .  �� Text �� 
 �� $ FindAncestor=�"  $ SelectedItem.DataBaseName��� -    ��Name$ TestConnectionButton��$��1��$��50��$��20��$
��Left=�$��0,0,0,0q�.��测试连接�� $��数据库：��$��0��$��2��$��Center=�$��Center=�$��Center=�$��Center=��� $��	间隔：��$��2��$��2��$��Center=�$��Center=�$��Center=�$��Center=��� -	   $ 
TimeSpanTB��$��3��$��2��$��15q����� 
 �� $ FindAncestor=�"  $ SelectedItem.TimeSpan�� $��1�� �� $
��auto ��� $��1* ��� $
��auto �.+��� $��Center=�$��Center=�SQL语句：�� -
   $ DSSQLTB��$��1��$��10q����	#FF1C4DE0���� 
 �� $ FindAncestor=�"  $ SelectedItem.SqlString�.���� $��2��$��Center=�$��Center=�$��	0,56,0,57q�.+��� -   $ DataViewButton��$��10q�$��80��$��30��.��预览�� -   $ SQLDeButton��$��10q�$��80��$��30��.��设计�� -   $ 
CopyButton��$��10q�$��80��$��30��.��复制�� -   $ SQLClearButton��$��10q�$��80��$��30��.��清空  $��本地文件��  $��网络资源��  $��静态文本��.���  �� �� .+��� -   $��1q����	#FF0072C6���� *StaticTextDataSourceList+
 �� *FindAncestor+"   �� View �� .P��� $��数据源名�� �� Width$	 200�� �� DisplayMemberBinding �� *Name+�� $��文本内容��$	 500�� �� *Text+� $��1�� �� $��1* ��� $��8* � �� $
��auto ��� $��1* �.+��� $��Right=�$��Center=�数据源名：�� -   $ StaticDSNameTB��$��1��$	��200��$
��Left=�$��5q����� *SelectedItem.Name+
 �� *FindAncestor+"  �� $��Right=�$��1��$	��Top=�	文本：�� -   $ StaticDSTextTB��$��1��$��1��$��5q����� *SelectedItem.Text+
 �� *FindAncestor+"  �� $��1��$ 
Horizontal=�$��Right=�.+��� -   $ ImportButton��$��80��$��20��$��10q�.��导入�� -   $ 	AddButton��$��80��$��20��$��10q�.��新建�� -   $ DeleteButton��$��80��$��20��$��10q�.��删除�� -   $ OKButton��$��80��$��20��$��10q�.��确定�� -   $ CancleButton��$��80��$��20��$��10q�.��取消