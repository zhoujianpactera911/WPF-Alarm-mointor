   M S B A M L   `   `   `  ����L  HZhaoxi.Industrial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Zhaoxi.IndustrialZhaoxi.Industrial  5   	   ?$clr-namespace:Zhaoxi.Industrial.ViewZhaoxi.Industrial.View  5   	   G(clr-namespace:Zhaoxi.Industrial.ConcoverZhaoxi.Industrial.Concover  5   	   "    Zhaoxi.Industrial.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Zhaoxi.Industrial     5   	   5view$clr-namespace:Zhaoxi.Industrial.View     5   	   ;local1(clr-namespace:Zhaoxi.Industrial.Concover     5   	   -   5        $��Microsoft YaHei)�5	      $��
ExtraLight"�67     � Title$  
MainWindow��5
   	   $	��750��6   $
��1300��6,   $
��1200��5   	   $	��750��6    � WindowStyle$
 None=�5   	    � AllowsTransparency .  � 
ResizeMode$ CanResizeWithGrip=�66    � WindowStartupLocation$ CenterScreen=�6U    � 	Resources 5       �� MergedDictionaries �� Source3   -Zhaoxi.Industrial.Concover.TabToBoolConverter   TabToBoolConverter �� 
TargetType  NavTabButtonStyle	 �� Property
 �� Value �� 
TargetType �� CornerRadius � ColumnDefinitions
 � Tag �� Triggers P� Property P� Value�� 5   
    5      �� 5      $( "./Assets/Styles/DefaultStyles.xaml?�6%   %M  &	        &	        5      6E   �� 5      " 	�6   .�5      �� 	 � ��$
 15(�6-   �� 5      	 � ��
 �#DDD6/   �� 5      	 � ��$

 10,5q�6,   �� 5      	 � ��$	
 5,0q�6+   �� 5      	 � ��

 ���� 6/   �� 5      	 � ��
 5      �� 5      " 	�6*   .R�5      �� #��z��6%   $ 5,5,0,0��6O   ._�5    "   �  5!   &   �� 5"   *   $
��auto �6;   �� 5#   *   .+�5%   &   �� #��z 60   $��./Assets/Fonts/#iconfont)�6M   $��14(�6s   $��Center=�5&   0   $��Center=�6K   $��10,0,0,0q�6h   �� 5'   &   $��1��67   #��z��6G   #��z��5(   #   #��z��5)   #   5+   #   5,      5-      5/       50      P� 51       �@d�	IsChecked$
 TrueY�63   . �52      �� 	 � ��

 �����67   �� 53      	 � ��
 �	#FFFF680067   54      56      57      .��59      � ��5:   
   
� 5;       
� Center$ 0.5,0.7&�6"    
� GradientOrigin$ 0.5,0.7&�63    
� RadiusY$	 0.8Q�6L    
� RadiusX$	 0.8Q�6Z   .��5<      �  � Color$ 	#FFA4B3D3��6   � 5=      $ 	#FF302C5D��6    � Offset$ 0.952Q�61   � 5>      $ 	#FF96AAD4��6   $ 0.163Q�61   5?       � RowDefinitions 5A   
   �� 5B      $	��100 �6   �� 5C      .+�5E   
   � $
��10,0q�6   .+�5F      �� $��1��6   $��55��6"   $��50��6-   $��	10,0,20,0q�69   $
��Left=�6L   ��5G      �� 5H       �� ImageSource$ ./Assets/Images/Logo.png��6!   5J      �� 5K      $��1��6   $��Center=�6)   .+�5L      �� $*��$朝夕科技节能管理控制系统��6   
�������$��25(�6C   $��85,0,0,0q�6Q   �� 5M      $��45��6   
������ ���	#FFFF6800$��0,0,0,1q�6V   $��	10,0,10,0q�5N      ._�5O      ��  �� Orientation$ 
Horizontal=�6!   $��Bottom=�6:   $��70,0,0,0q�6U   .+�5P      	� $��系统监控��6&   $	 ��65   #��[ 6D    d� 	IsChecked 6o   �� *	TabType+ �� 	Converter# [   �� ConverterParameter$ 1��	� 5Q      $��实时曲线��6&   $	 ��65   #��[ 6D    6o   �� *	TabType+# [  $ 2��	� 5R      $��历史曲线��6&   $	 ��65   #��[ 6D    6o   �� *	TabType+# [  $ 3��	� 5S      $��控制策略��6&   $	 ��65   #��[ 6D    6o   �� *	TabType+# [  $ 4��	� 5T      $��系统操作��6&   $	 ��65   #��[ 6D    6o   �� *	TabType+# [  $ 5��	� 5U      $��报警管理��6&   $	 ��65   #��[ 6D    6o   �� *	TabType+# [  $ 6��	� 5V      $��报表管理��6&   $	 ��65   #��[ 6D    6o   �� *	TabType+# [  $ 7��5W      5X      5Y      �� 5[      $��1��6   $��Right=�6)   $��Bottom=�6E   $ 
Horizontal=�6`   $��20,15q�5\      .+�5]      �� $��Center=�6   $��	10,0,10,0q�67   
��������� 5^      $�Jovan.Zhaoxi ��6   5_      �� 5`      $��30��6   $��30��6$   $ 15��60   $��1��6B    J� Effect 5a      3  -System.Windows.Media.Effects.DropShadowEffect  5b         
BlurRadius$ 5Q�6+       ShadowDepth$  0Q�6:   !   	Direction$! 0Q�6J   "   Opacity$	" 0.4Q�6X   #   Color$# White��6f   ��5d      �� 5e      $  ./Assets/Images/avatar.png��6%   5g      5h      5i      �� 5l   
   $ 
Horizontal=�6   $	��Top=�6.   $��Right=�6F   .+�5m      �� $��40��6   $��30��6    $	����6,   $��14(�6?     NormalButtonStyle#��[ 6M   
�������6x   �� 5n      $��40��6   $��30��6    $	����6,   $��10(�6?   #��[ 6M   
�������6x   �� 5o      -   5        $��50��5o      $��30��6    $	����6,   #��[ 6?   
�������6}   5p      � 5r   
   $��1��6   .+�5s      �� .��5t      *   $Zhaoxi.Industrial.View.SystemMonitor  5u      5v      5w      5x      