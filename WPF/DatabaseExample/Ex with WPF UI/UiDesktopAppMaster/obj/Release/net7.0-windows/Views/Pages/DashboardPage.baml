   M S B A M L   `   `   `  ���� M  IUiDesktopAppMaster, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:UiDesktopAppMaster.Views.PagesUiDesktopAppMaster.Views.Pages  2    ,UiDesktopAppMaster.Views.Pages.DashboardPage   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:UiDesktopAppMaster.Views.Pages     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf     N� Title$  DashboardPage��   ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �     TextFillColorPrimaryBrush   
Foreground# �   N� 
Foreground# �  .���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�� $��0��.+�  Wpf.Ui.Controls.Card  
���OO/�$��0,0,0,15q�.����  �� Orientation$ 
Horizontal=�$��0,0,0,0q�.+�   Wpf.Ui.Controls.SymbolIcon     Symbol$ Info28=�  Wpf.Ui.Controls.FontIcon   FontSize$ 18(�   
FontWeight$
 Bold"�$��0,0,5,0q�  Wpf.Ui.Controls.TextBlock  $
��Left=�$��Center=�$��15(�$
��Bold"�$5��/Welecome To Gangnamgu-Popultaion Analysis Lab !��� $��1��$��0,0,0,0q�.+��� .+��� $��● Rank Summary��$��15(�$��SemiBold"�$��0,0,0,15q�� $��2��$��0,0,0,15q�	 � ColumnDefinitions	 �� $��* ��� $��* ��� $��* ��� $��* �.+�� $��0��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��Top3 Total Population��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $�	신사동�� �� $�31,250��$��20(�$
��Bold"�
���\\��  $��0,0,0,10q�.��  $��0,0,0,0q��� $�	청담동�� �� $�29,280��$��20(�$
��Bold"�
���\\��  .��  $��0,0,0,0q��� $�	논현동�� �� $�28,775��$��20(�$
��Bold"�
���\\��� $��1��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��Top3 Male Population��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $�	삼성동�� �� $�10,235��$��20(�$
��Bold"�
���\\��  $��0,0,0,10q�.��  $��0,0,0,0q��� $�	개포동�� �� $�9,580��$��20(�$
��Bold"�
���\\��  .��  $��0,0,0,0q��� $�	도곡동�� �� $�8,775��$��20(�$
��Bold"�
���\\��� $��2��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��Top3 FeMale Population��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $�	논현동�� �� $�12,655��$��20(�$
��Bold"�
���\\��  $��0,0,0,10q�.��  $��0,0,0,0q��� $�	청담동�� �� $�10,450��$��20(�$
��Bold"�
���\\��  .��  $��0,0,0,0q��� $�	삼성동�� �� $�9,785��$��20(�$
��Bold"�
���\\��� $��3�� �� $
��auto �.+�  $��0,0,15,0q�.���� .+��� $��User Rating History��
�������$��14(�$��SemiBold"�$��0,0,0,15q�  $��0,0,0,10q�.���� $ 
Horizontal=�.+�  $��
Kelvin Lee��$��0,0,20,0q�#  Wpf.Ui.Controls.RatingControl    $��0,0,0,10q�.���� $ 
Horizontal=�.+�  $��Alex Kim��$��0,0,20,0q�    $��0,0,0,10q�.���� $ 
Horizontal=�.+�  $��	Jhon Park��$��0,0,20,0q�  � $��3��$��0,0,0,15q�.+��� .+��� $��● Search Details��$��15(�$��SemiBold"�$��0,0,0,15q��� $ 
Horizontal=�.+��� $	��235��$��0,0,15,0q�
 �� Text
 �� *(&ViewModel.SelectedAdministrativeAgency+���� * ViewModel.AdministrativeAgency+  Wpf.Ui.Controls.Button  $	��100��$
��auto��$��Search��
�������$��SemiBold"�$��15(�
���OO/����� *ViewModel.SearchDeatilCommand+� $��4��$��0,0,0,15q�	 �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+�� $��0��$��0,0,15,0q� �� $	��150 �.+�� $��0��.+�  .���� .+��� $��Total Population��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $��30(�$
��Bold"�
�����_���� *#!ViewModel.SelectedTotalPopulation+� $��1��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��Male Population��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $��30(�$
��Bold"�
�����_���� *" ViewModel.SelectedMalePopulation+� $��2��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��Female Population��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $��30(�$
��Bold"�
�����_���� *$"ViewModel.SelectedFeMalePopulation+� $��3��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��	Sex Ratio��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $��30(�$
��Bold"�
�����_���� *ViewModel.SelectedSexRatio+� $��4��$��0,0,15,0q� �� $
��auto �.+�� $��0��.+�  .���� .+��� $��Number of household��
�������$��14(�$��SemiBold"�$��0,0,0,10q�  $��0,0,0,10q�.��  $��0,0,0,0q��� $��30(�$
��Bold"�
�����_���� *&$ViewModel.SelectedNumberOfHouseholds+� $��5��$��0,20,0,0q�.+��� .+��� $��● Your Courses��$��15(�$��SemiBold"�$��0,0,0,15q�� $��6��$��0,0,0,0q�	 �� $��* ��� $��* ��� $��* �.+�� $��0��$��0,0,15,0q�.+�  .���� $��Center=�$��Center=�.+��� $ 
Horizontal=�$��0,0,0,10q�.+�  $ Link28=�$ 18(�$
 Bold"�$��0,0,5,0q�  $,��&Do you want to learn about WPF basic ?��$��15(�$��SemiBold"�$��0,0,0,0q�  $	��300��$��Click to Link!��$��15(�$��DemiBold"�
���� ��$��Center=�$��Center=�
����p��� $��1��$��0,0,15,0q�.+�  .���� $��Center=�$��Center=�.+��� $ 
Horizontal=�$��0,0,0,10q�.+�  $ Link28=�$ 18(�$
 Bold"�$��0,0,5,0q�  $$��How about Blazor WebAssembly ?��$��15(�$��SemiBold"�$��0,0,0,0q�  $	��300��$��Click to Link!��$��15(�$��DemiBold"�
���� ��$��Center=�$��Center=�
����p������ *%#ViewModel.GoToBlazorWasmLinkCommand+� $��2��$��0,0,15,0q�.+�  .���� $��Center=�$��Center=�.+��� $ 
Horizontal=�$��0,0,0,10q�.+�  $ Link28=�$ 18(�$
 Bold"�$��0,0,5,0q�  $%��Blazor ASP.NET Core also here !��$��15(�$��SemiBold"�$��0,0,0,0q�  $	��300��$��Click to Link!��$��15(�$��DemiBold"�
���� ��$��Center=�$��Center=�
����p��