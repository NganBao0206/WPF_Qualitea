   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   C  > :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI 6y     GUI.EmployeeOrder   6	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6F   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6�   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   9system&clr-namespace:System;assembly=mscorlib     6C  #localclr-namespace:GUI     6y    � Icon$  /asset/logo/greenline.ico��6%    � WindowStartupLocation$ CenterScreen=�6�   � WindowStyle$
 None=�6�   � AllowsTransparency . 
������  � Title$ Order��6   � WindowState$ 	Maximized=�6#  $��714.144��6;  $��1314.423��6L   � 	Resources 5       �� 
TargetType   disable �� Property	 �� Value 
 enable��%  &	        &	      �� 5   
   " ��6   .�5      ��  � ��	 �#aaa6+   �� 5       � ��$
	 Left=�64   -   5        5      �� 5   
   " ��6   .�5	      ��  � ��	 �	#FF53825E6+   �� 5
       � ��$	 Right=�64   -   5        5      5      .��5      �� ���#ffffff._�5   
   � .+�5      � $��0 20 0 0q�6   
 � ColumnDefinitions
 5      �� 5      $	��43* �6'   �� 5      $
��213* �6'   .+�5      �� -   5         ��Name$
 Tab3��5      
������� �� CornerRadius$ 0 0 30 0��6:    K� ZIndex$ 0��6R   $��0,0,0,-0.333q�6c   $��2��6y   ._�5      �  � RowDefinitions 5      �� 5      $��60 �6,   �� 5      $��80 �6,   �� 5      $��80 �6,   �� 5      
 5      �� 5      �� 5      $	��400 �6/   �� 5       $��2* �6/   .+�5"      �� -   5         ��Name$ BtnBack��5"   <   $
��Left=�6!   $��32��6M   $��32��6X   $��0q�6d     ButtonStyle1#���  6�    � Cursor$
 Hand��6�   $
��20 0q�6�   ��5#      �� 5$   "    �� ImageSource$ /asset/icons/back.png��6-   5&      �� 5'      $��1��6%    �� Orientation$ 
Horizontal=�65   $��Right=�6N   .+�5(      �� $��Lập hóa đơn��6(    	 Title#��[ 6;   $��0 0 0 0q�6Z    �� Padding$ 0q�6k   5)      �� 5*      -   5         ��Name$ comboBoxCategory��5*   #     ComboBoxStyle1#��[ 6=    �� SelectedValuePath$ 
CategoryID��6e    �� DisplayMemberPath$
 Name��6�   $��0��6�   $��1��6�   $��15q�6�   $2��,/Qualitea;component/fonts/#Montserrat Medium)�6�   $��50��6   $��Center=�6  $��	15 15 0 0q�6'  6;  �� 5,      $��1��6!   $��1��61     containTextBox#��[ 6>   ._�5-      �� $ 
Horizontal=�6)   .+�5.   "   �� -   5         ��Name$ searchBoxProduct��5.   k     	myTextBox#��[ 6*   $��10 0 0 0q�6M   $	��305��6_   6�   �� 5/   "   $��asset/icons/search.png��6(   $��10q�6H   50      51      �� 52      $��2��6!   $��2��65   ._�53      �� $ 
Horizontal=�6)   $��Center=�6B   $��50��6]   .+�54   "   �� $��1��6)   $��1��69   #��[ 6F   $��20 0 0 0q�6n   $	��380��6�   ._�55   &   �� $ 
Horizontal=�61   .+�56   *   �� $��Giá:��64     darkFont#��[ 6@   $��Center=�6b   $��10 0 0 0q�6}   �� 57   *   -   5        $ minPrice��57   2   #��[ 6D   $��0 0 0 1q�6g   ���	#FF434948$	��120��6�   $	��5 0q�6�   6�   �� 58   *   $��đ - ��64   #��[ 6@   $��Center=�6b   �� 59   *   -   5        $ maxPrice��59   2   #��[ 6D   $��0 0 0 1q�6g   ���	#FF434948$	��120��6�   $	��5 0q�6�   6�   �� 5:   *   $��đ��64   #��[ 6=   $��Center=�6_   5;   '   5<   #   �� 5=   "   -	   5        $��1��5=   )   ���	#FF53825E$��10q�6g   $
 Hand��6t   $��Right=�6�   $
��40 0q�6�   ��69   Z� .��5>   &   �� $ 
Horizontal=�61   .+�5?   *   �� $��Thêm vào đơn hàng ��64    
 myFont#��[ 6N   
�������$��Center=�6�   $	�� 18(�6�   �� 5@   *    �� Padding$ 0q�61   $	��3 0q�6=   ._�5A   .   �� $��asset/icons/cart.png��64   5B   +   5C   '   5D   #   5E      5F      �� 5G      $��3��6'   $��2��64   $��20 0 0 0q�6H   .��5H      �� .+�5I   "   �� -
   5         ��Name$ listProduct��5I   /   $��0 0 0 50q�6]   ��5J   &   �� 5K   *   .R�5L   .   �� $ 
productBox��65   $ 20 20 10 10��6I   $ 10q�6d   $	��200��6q   $��10 100 0 -20q�6}   ���	#FFE9EADC._�5M   2   �� .+�5N   6   �� ._�5O   :   �� $	��150��6^   $��	0 -80 0 5q�6k   ��6@   ��  �� Path$ Image� J� Effect 5P   >   3  -System.Windows.Media.Effects.DropShadowEffect  5Q   B      	Direction$	 245Q�6S      Color$ 	#00DADADA��6c      ShadowDepth$ 5Q�6u      
BlurRadius$ 20Q�6�   5S   ;   5T   7   �� 5U   6   $��0 10 0 2q�6[   #��[ 6m     K� Capitals$  AllSmallCaps=�6�   $��20(�6�   ��6@   �� $
 Name�6�   �� 5V   6   #��[ 6   $��14(�6�   ��6@   �� $ 	ProductID�! �� StringFormat$! Mã sản phẩm: {0}��6�   �� 5W   6   " ��Name$" 	PriceText��6@   #��[ 6�   $��14(�6�   ��6S   �� $ MinPrice�$! 	{0:N0} đ��6�   � 5X   6   
 5Y   :   �� 5Z   >   $��50 �6O   �� 5[   >   .+�5]   :   �� $��1��6y   $��Right=�6�   ��6G   �� $ ProductOptionsActive���5^   >   �� 5_   B   .R�5`   F   �� -   5        ���	#FF53825E$ 6q�5`   {   $��1 30 1 1q�6�   $ 10��6�   $
 Hand��6�   $��30��6�   $��30��6�   $��Center=�6�   ��6M   Z� ._�5a   J   �� $��14(�6o   $
��Bold"�6}   
�������# �� TextAlignment$# Center=�6�   ��6T   �� $
 Size�6�   5b   G   5c   C   ��5e   >   �� 5f   B   .R�5g   F   � 5h   C   5j   ;   5k   7   5l   3   5m   /   5n   +   ��5p   &   �� 5q   *   .R�5r   .   � 5s   +   5u   #   5v      5w      �� 5x      $��2��6!   $��4��61   ���	#FFE9EADC$��
30 30 30 0q�6Y   ._�5y      �  5z   "   �� 5{   &   �� 5|   &   $��70 �64   �� 5}   &   $	��150 �64   �� 5~   &   $��70 �64   .+�5�   "   �� -   5        $ ��Name$
$ Bill��5�   +   $
��Left=�69   $	��Top=�6T    	 ItemsPanelTemplate1#��� 	 6�   ��6l   Z� ��6�   Z� % �� View% 5�   &   �� 5�   *   .P�5�   .   �� $��ID��6=   & �� HeaderTemplate& 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��0��6S   $��ID��6c   $
��Left=�6m   $��Bottom=�6�   #��[ 6�   5�   7   ' �� CellTemplate' 5�   2   �� 5�   6   .R�5�   :   �� ._�5�   >   �� $��0��6H   $��Center=�6�   #��[ 6�   ��6X   �� $# ProductOption.ProductOptionID�6�   5�   ;   5�   7   5�   /   �� 5�   .   $
��Name��6=   & 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��1��6S   $��Tên sản phẩm��6c   $
��Left=�6w   $��Bottom=�6�   #��[ 6�   5�   7   ' 5�   2   �� 5�   6   .R�5�   :   �� ._�5�   >   �� $��0��6H   $��Center=�6�   #��[ 6�   ��6X   �� $  ProductOption.Product.Name�6�   5�   ;   5�   7   5�   /   �� 5�   .   $
��Size��6=   & 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��1��6S   $
��Size��6c   $
��Left=�6o   $��Bottom=�6�   #��[ 6�   5�   7   ' 5�   2   �� 5�   6   .R�5�   :   �� ._�5�   >   �� $��0��6H   $��Center=�6�   #��[ 6�   ��6X   �� $ ProductOption.Size�6�   5�   ;   5�   7   5�   /   �� 5�   .   $��Money��6=   & 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��1��6S   $��
Đơn giá��6c   $
��Left=�6r   $��Bottom=�6�   #��[ 6�   5�   7   ' 5�   2   �� 5�   6   .R�5�   :   �� ._�5�   >   �� $��0��6H   $��Center=�6�   #��[ 6�   ��6X   �� $ ProductOption._price�$! N0��6�   5�   ;   5�   7   5�   /   �� 5�   .   $��Quantity��6=   & 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��1��6S   $��SL��6c   $
��Left=�6m   $��Bottom=�6�   #��[ 6�   5�   7   ' 5�   2   �� 5�   6   .R�5�   :   �� $ 
Horizontal=�6E   .+�5�   >   �� $ btnMinus��6E   -   5        $��30��5�   W   $��30��6c    
 GreenButton#��[
 6n   $��5q�6�   .��5�   B   �� $��asset/icons/minus.png��6H   5�   ?   �� 5�   >   ._�5�   B   �� $��0��6L   $��Center=�6{   #��[ 6�   ��6\   �� $ Quantity�6�   5�   ?   �� 5�   >   $ btnAdd��6E   -   5        $��30��5�   U   $��30��6a   #��[
 6l   $��5q�6�   .��5�   B   �� $��asset/icons/add.png��6H   5�   ?   5�   ;   5�   7   5�   /   �� 5�   .   $��Money��6=   & 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��1��6S   $��Thành tiền��6c   $
��Left=�6u   $��Bottom=�6�   #��[ 6�   5�   7   ' 5�   2   �� 5�   6   .R�5�   :   �� ._�5�   >   �� $��0��6H   $��Center=�6�   #��[ 6�   ��6X   �� $ 
_totalLine�$! N0��6�   5�   ;   5�   7   5�   /   �� 5�   .   $	��Sta��6=   & 5�   2   �� 5�   6   .R�5�   :   �� $
 20 0q�6D   $��1��6S   $
��Left=�6c   $��Bottom=�6~   #��[ 6�   5�   7   ' 5�   2   �� 5�   6   .R�5�   :   �� $��50��6A   ._�5�   >   �� -   5        $��2��5�   E   $��30��6U   $��30��6`   ���	#FF53825E$ 10��6�   $ 5q�6�   $
 Hand��6�   $��8q�6�   ��6l   Z� ._�5�   B   �� $��asset/icons/trash.png��6H   5�   ?   5�   ;   5�   7   5�   /   5�   +   5�   #   �� 5�   "   $��1��6)   ._�5�   &   �� .+�5�   *   � 
 5�   .   �� 5�   2   �� 5�   2   .+�5�   .   �� $��Tổng tiền:��68   #��[ 6J   $
��20 0q�6l   �� 5�   .   -   5        $" txtTotal��5�   8   $��1��6J   $��Right=�6Z   $	��0đ��6v   #��[ 6�   $
��20 0q�6�   5�   +   � 5�   *   
 5�   .   �� 5   2   �� 5  2   .+�5  .   �� $��Số tiền giảm:��68   #��[ 6M   $
��20 0q�6o   �� 5  .   -   5        $" txtDiscount��5  8   $��1��6N   $��Right=�6^   $	��0đ��6z   #��[ 6�   $
��20 0q�6�   5  +   � 5  *   
 5  .   �� 5  2   �� 5	  2   .+�5  .   �� $��Thanh toán:��68   #��[ 6K   $��20,0,19.333,-0.667q�6j   $��16(�6�   $ 0q�6�   �� 5  .   -   5        $��txtCash��5  8   $��1��6G   $��Right=�6W   $	��0đ��6s   #��[ 6}   $��16(�6�   $
��20 0q�6�   $ 0q�6�   5  +   5  '   5  #   �� 5  "   $��2��6)   ._�5  &   � .+�5  *   �� ���	#9953825E$��2q�6I   ���	#FFE9EADC._�5  .   �� .+�5  2   �� $"��Mã khách hàng (nếu có)��6<   #��[ 6Z   $��20(�6y   $��Center=�6�   �� 5  2   $ 
Horizontal=�6=   .+�5  6   �� $��Nhập mã khách��6@   #��[ 6U   ( J� Opacity$	( 0.8Q�6w   $�� 30 0q�6�   $��Center=�6�   �� 5  6   -   5        $ 
idCustomer��5  >   $	��170��6R   
������ $��0 0 0 1q�6w   $��17(�6�   $��0q�6�   6�   �� 5  6   -   5        $ btnCheckCustomer��5  =   #��[
 6W   $��
Kiểm tra��6|   $	��100��6�   $��30��6�   $
��30 0q�6�   $��13(�6�   ���	#FF53825E���#FFF6�   5  3   5  /   5  +   �� 5  *   -   5        $ infoCustomer��5  �   ���	#FFE9EADC���	#9953825E$��2q�6`   $y�Hidden=�6t   ._�5  .   � $
��10 5q�63   
 5  2   �� 5   6   �� 5!  6    5#  2   �� 5$  6   �� 5%  6   .+�5'  2   �� -   5        $��x��5'  9     myDarkRoundBtn#��[ 6E   $��1��6m   $��30��6}   $��30��6�   $	��Top=�6�   $��Right=�6�   $ 2��6�   �� 5(  2   $ 
Horizontal=�6=   $��Center=�6V   .+�5)  6   �� #��[ 6@   $��Khách hàng: ��6b   �� 5*  6   $��5 0 0 0q�6@   $��2��6Q   #��[ 6�   ��6e   �� $
 Name�6�   5+  3   �� 5-  2   $��Center=�6=   $ 
Horizontal=�6X   $��1��6q   .+�5.  6   �� #��[ 6@   $��
Áp dụng��6b   $��Center=�6q   �� 5/  6   -   5        $" txtApplyScore��5/  @   #��[ 6W   $��Center=�6�   $��5 0 0 0q�6�   ��6y   �� $ Score�6�   �� 50  6   #��[ 6@   $��điểm��6b   $��Center=�6n   $��5 0 0 0q�6�   51  3   � 52  2   $��1��67   $��50��6G   $��20��6R   $��Right=�6^   $��1��6z   .+�53  6   �� -   5        $ ScoreApply_Bar��53  �   $��50��6=   $��10��6H   #��[  6T   $ 5��6u   $	( 0.5Q�6�   $��0,5,0,0q�6�   $	��Top=�6�   6�   �� 54  6   -   5        $ ScoreApply_Button��54  �   #��[  6=   $ 99��6^   $��20��6p   $��20��6{   6�   55  3   57  /   58  +   5:  '   5;  #   � 5<  "   $��3��6'   
 5=  &   �� 5>  *   �� 5?  *   .+�5A  &   �� -   5        $ 	btnCancel��5A  -     	RedButton#��[ 6@   $��Hủy đơn��6c   $	��200��6u   $��30��6�   $
��30 0q�6�   $��13(�6�   ���#FFF6�   �� 5B  &   -   5        $ btnPay��5B  -   $��1��6=   #��[
 6M   $��Thanh toán��6r   $	��200��6�   $��30��6�   $
��30 0q�6�   $��13(�6�   ���	#FF53825E���#FFF6�   5C  #   5D     5E     5F     5G     5H     5I     5J     5K     