   M S B A M L   `   `   `  ����D  @DbAsyncEx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DbAsyncEx	DbAsyncEx  5   	   9!clr-namespace:DbAsyncEx.ViewModelDbAsyncEx.ViewModel  5   	       DbAsyncEx.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DbAsyncEx     5   	   0vm!clr-namespace:DbAsyncEx.ViewModel     5   	     � Title$  
MainWindow��5	   	   $	��610��6   $	��660��6)    � 	Resources 5
      &    DbAsyncEx.ViewModel.MainWindowVM   VM��%   &	          5   
   6   5      .��5      � $��10q�6    � DataContext# [  6    � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5    
   �� $��0��6   $��10q�6%   .+�5!      �  � RowDefinitions 5"      �� 5#      $��* �6$   �� 5$      $
��Auto �6$   .+�5'      �� $��0��6   $��0,0,0,10q�6*   .+�5(      �� $��USERNAME��6    $��0,10,0,5q�60   �� 5)      -   5         ��Name$ usernametxtbox��5)      $��20��66   $��0,0,0,10q�6B   ��6T   �� *Name+�� 5+      $��
USERGENDER��6    $��0,10,0,5q�62   �� 5,      -   5        $ usergendertbox��5,      $��20��66   $��0,0,0,10q�6B   ��6T   �� *Gender+�� 5.      $��USERAGE��6    $��0,10,0,5q�6/   �� 5/      -   5        $ useragetxtbox��5/      $��20��65   $��0,0,0,10q�6A   ��6S   �� *Age+�� 51      $��USERJOB��6    $��0,10,0,5q�6/   �� 52      -   5        $ userjobyxtbox��52      $��20��65   $��0,0,0,10q�6A   ��6S   �� *Job+�� 54      $��USERMBTI��6    $��0,10,0,5q�60   �� 55      -   5        $ usermbtitxtbox��55      $��20��66   $��0,0,0,10q�6B   ��6T   �� *Mbti+�� 57      $
��Read��6   $��30��6,   $	��0,5q�68   
����ح���6\   ��  �� Path$ ReadButtonCommand��� 58      $��Create��6   $��30��6.   $	��0,5q�6:   
��������6_   �� $ CreateButtonCommand��� 59      $��Delete��6   $��30��6.   $	��0,5q�6:   
���������6_   �� $ DeleteButtonCommand�5:      �� 5;      $��1��6   .+�5<      �� $��	AsyncTest��6   $��30��61   $
��2,20q�6=   
���������6a   �� $ AsyncTestButtonCommand�{�6�   �� *AsyButtonIsEnable+� 5=      -   5         �Name$ AsyncProgressBar��5=   "   $��20��6<    � Value 6H   �� *ProgressValue+5>      5A      5B      &   System.Windows.Controls.DataGrid  5E   
   -   5        	  Name$	 dataGrid��5E      $��1��6%   
   AutoGenerateColumns$

 True��65   $��10q�6P      
IsReadOnly$
 True��6\      SelectionMode$ Single=�6n   ��6�   �� *
MyListUser+5F      5G      