   M S B A M L   `   `   `  ���� M  IUiDesktopAppMaster, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:UiDesktopAppMaster.Views.PagesUiDesktopAppMaster.Views.Pages  I)clr-namespace:UiDesktopAppMaster.ControlsUiDesktopAppMaster.Controls  E'clr-namespace:UiDesktopAppMaster.ModelsUiDesktopAppMaster.Models  -    'UiDesktopAppMaster.Views.Pages.DataPage   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:UiDesktopAppMaster.Views.Pages     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     >controls)clr-namespace:UiDesktopAppMaster.Controls     :models'clr-namespace:UiDesktopAppMaster.Models     A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml    N� Title$  DataPage��   ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �     TextFillColorPrimaryBrush   
Foreground# �   N� 
Foreground# �  $��False��.���  � ColumnDefinitions �� $	��300 ��� $��* �.+�0   *UiDesktopAppMaster.Controls.LoadingControl  -     Name$ searchGridLoadingControl��$��0��$��Center=�$y�Visible=�� -    �Name$ 
searchGrid��$y�	Collapsed=�$��0�� � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�  Wpf.Ui.Controls.TextBlock  -     Name$ tbkId��$��0��$��	0,0,10,10q��� $	�Id �� �� $�*��
��� E��  Wpf.Ui.Controls.NumberBox  $��1��$��	0,0,10,10q����� *ViewModel.SelectedId+	 �� Mode$	 TwoWay=�  -   $ tbkAdminAgency��$��2��$��	0,0,10,10q��� $�Administrative Agency �� �� $�*��
��� E���� -   
 ��Name$
 cbxAdminAgency��$��3�� �� 
IsEditable$
 True��$��	0,0,10,10q� �� Text �� *(&ViewModel.SelectedAdministrativeAgency+���� * ViewModel.AdministrativeAgency+$	 TwoWay=�  -   $ tbkTotalPopulation��$��4��$��	0,0,10,10q��� $�Total Population �� �� $�*��
��� E��  -     Name$ tbxTotPopulation��$��5��$��	0,0,10,10q����� *#!ViewModel.SelectedTotalPopulation+$	 TwoWay=�  -   $ tbkMalePopulation��$��6��$��	0,0,10,10q��� $�Male Population �� �� $�*��
��� E��  -	   $ tbxMalePopulation��$��7��$��	0,0,10,10q����� *" ViewModel.SelectedMalePopulation+$	 TwoWay=�  -
   $ tbkFemalePopulation��$��8��$��	0,0,10,10q��� $�FeMale Population �� �� $�*��
��� E��  -   $ tbxFemalePopulation��$��9��$��	0,0,10,10q����� *$"ViewModel.SelectedFeMalePopulation+$	 TwoWay=�  -   $ tbkSexRatio��$��10��$��	0,0,10,10q��� $�
Sex Ratio �� �� $�*��
��� E��  $��11��$��	0,0,10,10q����� *ViewModel.SelectedSexRatio+$	 TwoWay=�  -   $ tbkNumberOfHouseholds��$��12��$��	0,0,10,10q��� $�Number Of Households �� �� $�*��
��� E��  $��13��$��	0,0,10,10q����� *&$ViewModel.SelectedNumberOfHouseholds+$	 TwoWay=�  -   $$ tbkNumberOfPeoplePerHouseholds��$��14��$��	0,0,10,10q��� $&� Number Of People Per Households �� �� $�*��
��� E��  $��15��$��	0,0,10,10q����� */-ViewModel.SelectedNumberOfPeoplePerHouseholds+$	 TwoWay=�  Wpf.Ui.Controls.Button  $��16��$��50��$	��290��$��Create��
�������
�����F�$��	0,0,10,10q����� * ViewModel.CreateNewDataCommand+� $��17�� �� $��* ��� $��* �.+�  $��0��$��50��$	��140��$��Read All��
�������
���2͚�$��Center=�$��	0,0,10,10q����� *ViewModel.ReadAllDataCommand+  $��1��$��50��$	��140��$
��Read��
�������
���2͚�$��Center=�$��	0,0,10,10q����� *!ViewModel.ReadDetailDataCommand+  $��18��$��50��$	��290��$��Update��
�������
�������$��	0,0,10,10q����� *ViewModel.UpdateDataCommand+  $��19��$��50��$	��290��$��Delete��
�������
���\\��$��	0,0,10,10q����� *ViewModel.DeleteDataCommand+  -   $ dgGridLoadingControl��$��1��$��Center=�$y�Visible=�� -   $ dgGrid��$y�	Collapsed=�$��1��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgGangnamguPopulation������ * ViewModel.GangnamguPopulations+$	 OneWay=�