   M S B A M L   `   `   `  ����M  IUiDesktopAppMaster, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:UiDesktopAppMaster.Views.PagesUiDesktopAppMaster.Views.Pages  5      I)clr-namespace:UiDesktopAppMaster.ControlsUiDesktopAppMaster.Controls  5      E'clr-namespace:UiDesktopAppMaster.ModelsUiDesktopAppMaster.Models  5	      -    'UiDesktopAppMaster.Views.Pages.DataPage   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:UiDesktopAppMaster.Views.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      >controls)clr-namespace:UiDesktopAppMaster.Controls     5      :models'clr-namespace:UiDesktopAppMaster.Models     5	      A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5
        N� Title$  DataPage��5         ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �   5        TextFillColorPrimaryBrush   
Foreground# �  5       N� 
Foreground# �  5      $��False��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $��* �6   .+�5   
   0   *UiDesktopAppMaster.Controls.LoadingControl  -   5          Name$ searchGridLoadingControl��5   P   $��0��6"   $��Center=�63   $y�Visible=�6r   � 5   
   -   5         �Name$ 
searchGrid��5      $y�	Collapsed=�6#   $��0��6:    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5       $
��auto �6    �� 5!      $
��auto �6    �� 5"      $
��auto �6    �� 5#      $
��auto �6    �� 5$      $
��auto �6    �� 5%      $
��auto �6    �� 5&      $
��auto �6    �� 5'      $
��auto �6    �� 5(      $
��auto �6    �� 5)      $
��auto �6    �� 5*      $
��auto �6    �� 5+      $
��auto �6    �� 5,      $
��auto �6    �� 5-      $
��auto �6    �� 5.      $
��auto �6    �� 5/      $
��auto �6    �� 50      $
��auto �6    �� 51      $
��auto �6    .+�54        Wpf.Ui.Controls.TextBlock  -   5          Name$ tbkId��54   (   $��0��6   $��	0,0,10,10q�67   �� 55      $	�Id ��6    �� 56      $�*��6   
��� E��6   57        Wpf.Ui.Controls.NumberBox  59      $��1��6   $��	0,0,10,10q�6[   ��6(   �� *ViewModel.SelectedId+	 �� Mode$	 TwoWay=�6[     5;      -   5        $ tbkAdminAgency��5;   (   $��2��6   $��	0,0,10,10q�6@   �� 5<      $�Administrative Agency ��6    �� 5=      $�*��6   
��� E��6   5>      �� 5@      -   5        
 ��Name$
 cbxAdminAgency��5@   $   $��3��6    �� 
IsEditable$
 True��6<   $��	0,0,10,10q�6�    �� Text 6N   �� *(&ViewModel.SelectedAdministrativeAgency+��6�   �� * ViewModel.AdministrativeAgency+$	 TwoWay=�6�     5B      -   5        $ tbkTotalPopulation��5B   (   $��4��6   $��	0,0,10,10q�6D   �� 5C      $�Total Population ��6    �� 5D      $�*��6   
��� E��6   5E        5F      -   5          Name$ tbxTotPopulation��5F   (   $��5��6   $��	0,0,10,10q�6�   ��6B   �� *#!ViewModel.SelectedTotalPopulation+$	 TwoWay=�6�     5H      -   5        $ tbkMalePopulation��5H   (   $��6��6   $��	0,0,10,10q�6C   �� 5I      $�Male Population ��6    �� 5J      $�*��6   
��� E��6   5K        5L      -	   5        $ tbxMalePopulation��5L   (   $��7��6   $��	0,0,10,10q�6�   ��6C   �� *" ViewModel.SelectedMalePopulation+$	 TwoWay=�6�     5N      -
   5        $ tbkFemalePopulation��5N   (   $��8��6   $��	0,0,10,10q�6E   �� 5O      $�FeMale Population ��6    �� 5P      $�*��6   
��� E��6   5Q        5R      -   5        $ tbxFemalePopulation��5R   (   $��9��6   $��	0,0,10,10q�6�   ��6E   �� *$"ViewModel.SelectedFeMalePopulation+$	 TwoWay=�6�     5T      -   5        $ tbkSexRatio��5T   )   $��10��6   $��	0,0,10,10q�6>   �� 5U      $�
Sex Ratio ��6    �� 5V      $�*��6   
��� E��6   5W        5X      $��11��6   $��	0,0,10,10q�6b   ��6)   �� *ViewModel.SelectedSexRatio+$	 TwoWay=�6b     5Z      -   5        $ tbkNumberOfHouseholds��5Z   )   $��12��6   $��	0,0,10,10q�6H   �� 5[      $�Number Of Households ��6    �� 5\      $�*��6   
��� E��6   5]        5^      $��13��6   $��	0,0,10,10q�6l   ��6)   �� *&$ViewModel.SelectedNumberOfHouseholds+$	 TwoWay=�6l     5`      -   5        $$ tbkNumberOfPeoplePerHouseholds��5`   )   $��14��6   $��	0,0,10,10q�6Q   �� 5a      $&� Number Of People Per Households ��6    �� 5b      $�*��6   
��� E��6   5c        5d      $��15��6   $��	0,0,10,10q�6u   ��6)   �� */-ViewModel.SelectedNumberOfPeoplePerHouseholds+$	 TwoWay=�6u     Wpf.Ui.Controls.Button  5f      $��16��6   $��50��6&   $	��290��62   $��Create��6q   
�������
�����F�$��	0,0,10,10q�6�   ��6>   �� * ViewModel.CreateNewDataCommand+6�   � 5h      $��17��6    5i      �� 5j      $��* �6'   �� 5k      $��* �6'   .+�5m        $��0��6   $��50��6,   $	��140��68   $��Read All��6u   
�������
���2͚�$��Center=�6�   $��	0,0,10,10q�6�   ��6D   �� *ViewModel.ReadAllDataCommand+6�     5n      $��1��6   $��50��6,   $	��140��68   $
��Read��6x   
�������
���2͚�$��Center=�6�   $��	0,0,10,10q�6�   ��6D   �� *!ViewModel.ReadDetailDataCommand+6�   5o        5q      $��18��6   $��50��6&   $	��290��62   $��Update��6n   
�������
�������$��	0,0,10,10q�6�   ��6>   �� *ViewModel.UpdateDataCommand+6�     5s      $��19��6   $��50��6&   $	��290��62   $��Delete��6n   
�������
���\\��$��	0,0,10,10q�6�   ��6>   �� *ViewModel.DeleteDataCommand+6�   5t        5v   
   -   5        $ dgGridLoadingControl��5v   P   $��1��6"   $��Center=�63   $y�Visible=�6n   � 5x   
   -   5        $ dgGrid��5x      $y�	Collapsed=�6   $��1��66   .+�5y      &   System.Windows.Controls.DataGrid  -   5          Name$ dgGangnamguPopulation��5y      ��66   �� * ViewModel.GangnamguPopulations+$	 OneWay=�5z      5{      5|      