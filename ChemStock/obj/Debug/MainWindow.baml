   M S B A M L   `   `   `  ����D  @ChemStock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ChemStock	ChemStock  5   	       ChemStock.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ChemStock     5   	     � Title$5  /ChemStock - Управление складом��5   	    � Icon$ assets/icon.ico��5	   	   $	��600��5
   	   $
��1000��6    � WindowStartupLocation$ CenterScreen=�5   	   $��Segoe UI)�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���	#FF2C3E50._�5      �� .+�5      �� $��	ChemStock��6   
�������$��18(�5      $
��Bold"�5      $��10 20q�5      $��Center=�5      �� 5      -   5        $��
MainButton��5   7   $��Главная��6   $��40��6+   6I   �� 5      -   5        $��	CatButton��5   9   $��Категории��6   $��40��6-   6J   �� 5       -   5        $��EmployerButton��5    :   $��Сотрудники��6   $��40��6.   6P   �� 5"      $��Bottom=�6   $��0 0 0 20q�68   .+�5#      �� -   5        $��
exitButton��5#   P   $��
Выход��6   $��40��6-   ���	#FFE74C3C6b   5$      5%      5&      � 5)   
   $��1��6   ���	#FF69778E � RowDefinitions 5*      �� 5+      $
��Auto �6    �� 5,      $��* �6    .+�50       � -   5          �Name$ 	MainFrame��50      $��1��6'     � NavigationUIVisibility$ Hidden=�64   51      52      53      