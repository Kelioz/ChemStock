   M S B A M L   `   `   `  ����  	ChemStock    ChemStock.UsersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$7  1Управление пользователями��5       N� 
Background �	#FFF0F0F0 � 	Resources 5      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader   ActionButtonsTemplate �� Orientation � ToolTip
 � Tag��%:  '        '  �    &	  �    �� 5   
   "  6   .�5      ��  � ��
 �����6+   �� 5       � �� �	#FFCCCCCC6,   �� 5       � ��$ 1q�60   �� 5       �@ RowBackground
 �����6.   �� 5        �@ AlternatingRowBackground �	#FFF5F5F569   �� 5       �@ HeadersVisibility$ Column=�62   �� 5       �@ CanUserAddRows .  6/   �� 5       � ��$ 10q�6'   �� 5       � ��$ 14(�6)   5      �� 5   
   "  6   .�5      ��  � �� �	#FF2C3E506+   �� 5       � ��
 �����6+   �� 5       � ��$
 10 5q�6(   �� 5       � ��$
 Bold"�6+   5      �� 5!   
   .R�5"      �� $ 
Horizontal=�6   .+�5#      �� -   5        $��✏️��5#      $  Редактировать��5$      
������ $��0q�5(      ���	#FF3A4F63 5&      �� *id+5)      �� 5*      -   5        $��🗑️��5*      $ Удалить��5+      
������ $��0q�5/      ���	#FFE74C3C 5-      �� *id+50      51      52      53      .��55      � 	 � RowDefinitions	 56   
   �� 57      $
��Auto �6   �� 58      $��* �6   .+�5<   
   �� $��0��6   $ 
Horizontal=�6"   $��Right=�6;   $��	0 10 10 0q�6W   .+�5=      �� -   5        $��Добавить��5=      $	��120��6(   $��30��64   6@   �� 5>      -   5        $��Обновить��5>      $	��120��6(   $��30��64   $��10 0 0 0q�6@   6R   5?        5B   
   -   5        
  Name$
 	UsersGrid��5B      $��1��6&      AutoGenerateColumns .     Columns 5C      0  *System.Windows.Controls.DataGridTextColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H      
Visibility$ 	Collapsed=�6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *id+6U     5E      $ ФИО��6%   $ * 6J    62   �� *ФИО+6J     5F      $ Телефон��6%   $	 150 6R    66   �� *Телефон+6R     5G      $ Роль��6%   $	 120 6L    63   �� *
Роль+6L     5H      $ 
Логин��6%   $	 120 6N    64   �� *
Логин+6N     5I      $ Пароль��6%   $	 120 6P    65   �� *Пароль+   ElementStyle 5J      �� 5K      " ��6    .�5L      ��  � ��$ ••••••��65   5M      5O      4  .System.Windows.Controls.DataGridTemplateColumn  5P      $ Действия��6)   $
 Auto 6;      CellTemplate# [  6H   5R      5S      5T      