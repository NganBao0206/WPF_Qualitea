   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5   7      GUI.Customer   6	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6:   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6|   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   #localclr-namespace:GUI     67  -   5        $
��1080��5       $	��720��6-     � Title$  Customer��6h   � WindowStyle$
 None=�6y  $y�Visible=�6�   � 
ResizeMode$ CanResizeWithGrip=�6�   � WindowStartupLocation$ CenterScreen=�6�   � AllowsTransparency .  � WindowState$ 	Maximized=�6,   � Icon$ /asset/logo/greenline.ico��6D  ��6�  Z�  � 	Resources 5       �� 
TargetType   disable	 �� Property
 �� Value 
 enable��%  &	        &	      �� 5   
   " ��6   .�5      �� 	 � ��
 �#aaa6+   �� 5      	 � ��$

 Left=�64   -   5        5      �� 5   
   " ��6   .�5	      �� 	 � ��
 �	#FF53825E6+   �� 5
      	 � ��$
 Right=�64   -   5        5      5      .��5      �� ���	#FFF1F1F1 �� CornerRadius$ 5��6$   ._�5   
   �  � RowDefinitions 5      �� 5      $��0 �6    �� 5      .+�5.      �� $��1��6   ._�5/      � .+�50      �  � ColumnDefinitions 51      �� 52      $
��0.8* �6/   �� 53      .+�55      ��  �� Padding$ 20q�6!   ���	#FF53825E$ 5 0 0 5��6E   56      � 57       58      �� 59      $	��100 �6,   �� 5:      .+�5<      � $��0��6   .+�5=      � $��0��6#    5>   "   �� 5?   &   $
��0.8* �67   �� 5@   &   .+�5B   "   �� $ 20q�6)   ._�5C   &   �� $��asset/logo/whiteline2.png��6,   5D   #   � 5E   "   -   5         �Name$
 menu��5E   7   $��1��6'    5F   &   �� 5G   *   �� 5H   *   �� 5I   *   �� 5J   *   �� 5K   *   .+�5M   &   �� -   5        $��0��5M   -   $��Center=�6=    � Cursor$
 Hand��6Z   $��Center=�6h   ._�5N   *   ��   	greenFont#��[ 64   $
��Home��6W   5O   '   �� 5P   &   -   5         ��Name$
 btn2��5P   -   $��1��6;   $��Center=�6K   $
 Hand��6h   $��Center=�6v   ._�5Q   *   ��   darkFont#��[ 64   $��Đặt hàng��6V   5R   '   �� 5S   &   -   5        $��2��5S   -   $��Center=�6=   $
 Hand��6Z   $��Center=�6h   ._�5T   *   �� #��[ 64   $��Đơn hàng��6V   5U   '   �� 5V   &   -   5        $��3��5V   -   $��Center=�6=   $
 Hand��6Z   $��Center=�6h   ._�5W   *   �� #��[ 64   $��
Thông tin��6V   5X   '   �� 5Y   &   -	   5        $ Logout��5Y   -   $��5��6=   $��Center=�6M   $
 Hand��6j   $��Center=�6x   ._�5Z   *   �� #��[ 64   $��Đăng xuất��6V   5[   '   5\   #   5^      5_      � 5`      -
   5        $
 tab1��5`   ,   $��1��6    K� ZIndex$ 1��6:   .+�5a      �  5b   "   �� 5c   &   $
��0.8* �67   �� 5d   &   .+�5f   "   �� ���	#FF53825E$ 0 0 0 5��6@   �� 5g   "   $��1��6)   ���	#FFF1F1F15h   #   5i      � 5j      -   5        $ firstProduct��5j   #    5k   "   �� 5l   &   $��1* �67   �� 5m   &   �� 5n   &   $
��1.4* �67   .+�5p   "   � $��0��6'   .+�5q   &   �� $(��"CHÀO MỪNG ĐẾN VỚI QUALITEA��60     regularFont#��[ 6R   $��55(�6w   $��WrapWithOverflow=�6�    �� Padding$ 30q�6�   $��Center=�6�   5r   #   � 5s   "   $��1��6'   .+�5t   &   �� ._�5u   *   �� $��-100 0q�6I   ��60   �� *Image+6I   5v   '   5w   #   � 5x   "   $��2��6'    5y   &   �� 5z   *   �� 5{   *   $��3* �68   �� 5|   *   .+�5~   &   �� $��1��6-   $��Center=�6:   ._�5   *   �� .+�5�   .   ��  	 Title#��[ 6N   $��40(�6m   $
 0 10q�6{   ��68   �� *Name+6{   �� 5�   .   #��[ 6q   $	 0 0q�6�   ��68   �� *
MinPrice+ �� StringFormat$ Giá từ {0:N0}đ��6�   �� 5�   .   -   5         ��Name$ bookProduct��5�   �   $��Đặt ngay��65     SignInButton#��[ 6H   $
��Left=�6o   6�   5�   +   5�   '   5�   #   5�      5�      � 5�      -   5        $
 tab2��5�   ,   $��1��6   .+�5�      �  5�   "   �� 5�   &   $
��0.8* �67   �� 5�   &   .+�5�   "   �� ���	#FF53825E$ 0 0 0 5��6@   �� 5�   "   $��1��6)   ���	#FFF1F1F15�   #   5�      � 5�       5�   "   �� 5�   &   $
��0.8* �67   �� 5�   &   .+�5�   "   � -   5        $ mainProduct��5�   '    5�   &   �� 5�   *   �� 5�   *   $��3* �68   �� 5�   *   �� 5�   *   �� 5�   *    5�   &   �� 5�   *   �� 5�   *   .+�5�   &   �� -   5        $ beforeChoose��5�   z   $��2��6-   $��5��6A   ���	#FF53825E$ 1��6i   $ 0 0 0 5��6�   ._�5�   *   �� $&�� Mời bạn chọn thức uống��64   #��[ 6R   $��55(�6w   $��WrapWithOverflow=�6�   $ 30q�6�   $��Center=�6�   5�   '   �� 5�   &   $��2��6-   $��5��6A   ���	#FF53825E$ 0��6i   $ 0 0 0 5��6z   5�   '   �� 5�   &   $��1��6-   ._�5�   *   �� -   5         ��Name$ 
imgMainPrd��5�   N   ��60   ��  �� Path$ Image�6N   5�   '   �� 5�   &   $��1��6-   $��1��6:   $ 10q�6J   ._�5�   *   �� -   5         ��Name$ nameMainPrd��5�   �   $��Center=�6O   #��[ 6j   $��40(�6�   $��WrapWithOverflow=�6�   ��64   �� $
 Name�6�   5�   '   �� 5�   &   $��2��6-   $��1��6:   $ 10q�6J   ._�5�   *   �� -   5        $ priceMainPrd��5�   �   $��Center=�6l   #��[ 6�   $��30(�6�   $��WrapWithOverflow=�6�   ��64   �� $ MinPrice�$ {0:N0}đ��6�   5�   '   �� 5�   &   -   5         ��Name$ optionMainPrd��5�   �   $��2��63   $��0��6@   $��Center=�6P   $��Center=�6m   ��6�   �� $ ProductOptionsActive���5�   *   �� 5�   .   .R�5�   2   �� -   5        
�������$��2q�5�   M   $ 10q�6a   $	��5 0q�6n   $ 5��6{   $
 Hand��6�   $��50��6�   $��50��6�   $��Center=�6�   ._�5�   6   �� #��[ 6[    �� TextAlignment$ Center=�6�   $��Center=�6�   $��Center=�6�   $��20(�6�   ��6@   �� $
 Size�6�   5�   3   5�   /   ��5�   *   �� 5�   .   .R�5�   2   � 5�   /   5�   '   �� 5�   &   $��3��6-   $
 40 0q�6:   ._�5�   *   �  5�   .   �� 5�   2   �� 5�   2   �� 5�   2   .+�5�   .   �� -   5        $ minus��5�   |   $��-��65     
myRoundBtn#��[ 6A   $��50��6e   $��50��6p   6�   �� 5�   .   -   5         ��Name$ quantity��5�   �   $��1��66     	myTextBox#��[ 6F   
�������$��1��6|   $��0q�6�   $��Center=�6�   6�   �� 5�   .   -   5        $
 plus��5�   �   $��2��65   $��+��6E   #��[ 6Q   $��50��6u   $��50��6�   6�   5�   +   5�   '   �� 5�   &   -   5        $ 	addToCard��5�   �   $��Thêm vào giỏ��6-   $��3��6D   $��1��6Q    		 myBtn#��[	 6a   $��
0 10 40 10q�6�   6�   5�   #   �� 5�   "   $��1��6)   ���	#FFF1F1F1$ 0 0 30 0��6P   $ 0��6h   ._�5�   &   �  5�   *   �� 5�   .   $��80 �6<   �� 5�   .    5�   *   �� 5�   .   �� 5�   .   $	��400 �6?   .+�5�   *   �� -   5         ��Name$ comboBoxCategory��5�   @   $��0��63    
 ComboBoxStyle3#��[
 6Z    �� SelectedValuePath$ 
CategoryID��6�    �� DisplayMemberPath$
 Name��6�   $��0��6�   $��15q�6�   $2��,/Qualitea;component/fonts/#Montserrat Medium)�6�   $��50��6  $��Center=�6  $��	15 15 0 0q�67  6K  �� 5�   *   $��1��61   $��0��6A     containTextBox#��[ 6N   ._�5�   .   ��  �� Orientation$ 
Horizontal=�69   .+�5�   2   �� -   5        $ searchBoxProduct��5�   {   #��[ 6:   $��10 0 0 0q�6]   $	��305��6o   6�   �� 5�   2   $��asset/icons/search.png��68   $��10q�6X   5�   /   5�   +   �� 5�   *   $��1��67   $��2��6D   $��20 0 0 0q�6X   .��5�   .   �� .+�5�   2   �� -   5        $ listProduct��5�   ?   $��0 0 0 0q�6m   ��5�   6   �� 5�   :   .R�5�   >   �� $ 
productBox��6E   $ 20 20 10 10��6Y   $ 10q�6t   $	��170��6�   $��10 100 0 -20q�6�   ���	#FFE9EADC._�5�   B   �� .+�5�   F   �� ._�5�   J   �� $	��150��6n   $��	0 -80 0 5q�6{   ��6P   �� $ Image�  J� Effect  5�   N   3  -System.Windows.Media.Effects.DropShadowEffect  5�   R   !   	Direction$	! 245Q�6c   "   Color$" 	#00DADADA��6s   #   ShadowDepth$# 5Q�6�   $   
BlurRadius$$ 20Q�6�   5�   K   5�   G   �� 5�   F   $��CharacterEllipsis=�6k   $��0 10 0 2q�6�   #��[ 6�   % K� Capitals$% AllSmallCaps=�6�   $��20(�6�   ��6P   �� $
 Name�6�   �� 5�   F   $ 	PriceText��6P   #��[ 6�   $��16(�6�   ��6c   �� $ MinPrice�$ {0:N0}đ��6�   � 5�   F    5�   J   �� 5�   N   $��* �6_   �� 5�   N   $
��auto �6_   .+�5�   J   �� -   5        ���	#FF53825E$ 5��5�   h   $��0 5 0 0q�6y   $��Center=�6�   $��Right=�6�   $ 5q�6�   $
 Hand��6�   ._�5�   N   �� $��Chọn��6X   #��[ 6d   $��Center=�6�   5�   K   5�   G   5�   C   5�   ?   5�   ;   ��5�   6   �� 5�   :   .R�5�   >   � 5   ;   5  3   5  /   5  +   5  '   5  #   5     �� 5     -   5        $��1��5  %   $��40��62   $��	0 0 10 10q�6=   $ 99��6P   $��Bottom=�6b   $��Right=�6}   $
 Hand��6�   ._�5	  "   �� $��/asset/icons/bag.png��6(   5
     5     � 5     -   5        $
 tab3��5  ,   $��1��6   $ 0��6:   .+�5     �  5  "   �� 5  &   $
��0.8* �67   �� 5  &   .+�5  "   �� ���	#FF53825E$ 0 0 0 5��6@   �� 5  "   $��1��6)   ���	#FFF1F1F15  #   5     � 5      5  "   �� 5  &   $
��0.8* �67   �� 5  &   .+�5  "   �� -   5        $ 	mainOrder��5  )   ._�5  &   � .+�5  *   �� -    5        $ beforeOrderView��5  �   $��2��61   $��8��6E   $ 1��6V   $ 0 0 0 5��6g   ���	#FF53825E._�5  .   �� $8��2Mời bạn chọn hóa đơn để xem thông tin��68   #��[ 6e   $��55(�6�   $��WrapWithOverflow=�6�   $ 30q�6�   $��Center=�6�   5   +   �� 5!  *   
�������$��30q�6D   $ 30q�6P   $ 10��6]   ._�5"  .   �  5#  2   �� 5$  6   �� 5%  6   �� 5&  6   �� 5'  6   �� 5(  6   �� 5)  6   �� 5*  6   $��3* �6D   �� 5+  6   �� 5,  6   $
��1.2* �6D   .+�50  2   � 
������� 51  6   �� 52  :   $
��0.7* �6K   �� 53  :   .+�55  6   �� $��Mã đơn hàng: ��6@   #��[ 6U   $
��None{�6x   �� 56  6   $��1��6@   #��[ 6t   ��6P   �� $ OrderHeaderID�6t   57  3   � 58  2   $��1��67    59  6   �� 5:  :   $
��0.7* �6K   �� 5;  :   .+�5=  6   �� $��Thời gian đặt: ��6@   #��[ 6W   $
��None{�6z   �� 5>  6   $��1��6@   #��[ 6�   ��6P   �� $ 	OrderDate�$ {0:hh\hmm d/M/yyyy}��6�   5?  3   � 5@  2   $��2��67    5A  6   �� 5B  :   $
��0.7* �6K   �� 5C  :   .+�5E  6   �� $��Tên người nhận: ��6@   #��[ 6X   $
��None{�6{   �� 5F  6   $��1��6@   #��[ 6|   ��6P   �� $ ShipInfo.ReceiverName�6|   5G  3   � 5H  2   $��3��67    5I  6   �� 5J  :   $
��0.7* �6K   �� 5K  :   .+�5M  6   �� $��Địa chỉ nhận: ��6@   #��[ 6V   $
��None{�6y   �� 5N  6   $��1��6@   #��[ 6w   ��6P   �� $ ShipInfo.Address�6w   5O  3   � 5P  2   $��4��67    5Q  6   �� 5R  :   $
��0.7* �6K   �� 5S  :   .+�5U  6   �� $��Địa chỉ nhận: ��6@   #��[ 6V   $
��None{�6y   �� 5V  6   $��1��6@   #��[ 6}   ��6P   �� $ ShipInfo.ReceiverPhone�6}   5W  3   � 5X  2   $��5��67    5Y  6   �� 5Z  :   $
��0.7* �6K   �� 5[  :   .+�5]  6   �� $��Thanh toán��6@   #��[ 6R   $
��None{�6u   �� 5^  6   $��1��6@   #��[ 6�   ��6P   �� $
 Cash�$ {0:N0}đ��6�   5_  3   � 5`  2   $��6��67    5a  6   �� 5b  :   $��25 �6H   �� 5c  :   .+�5e  6   �� $��0��6@   $��Sản phẩm đã đặt��6M   #��[ 6d   $
��None{�6�   �� 5f  6   -!   5        $ containerDetail��5f  J   $��1��6=   ���	#2653825E$ 10q�6z   $ 10��6�   ._�5g  :   �� $	��150��6G   $	��Top=�6W   ��6o   �� *ActualHeight+& �� ElementName$& containerDetail��' �� Mode$' OneWay=�.��5h  >   �� .+�5i  B   �� $��0 0 0 0q�6y   ��6O   �� $ OrderDetails���5j  F   �� 5k  J   .R�5l  N   �� $	 0 3q�6U   ._�5m  R   �  5n  V   �� 5o  Z   $
��1.5* �6k   �� 5p  Z   $
��0.5* �6k   �� 5q  Z   .+�5s  V   �� $��0��6]   ._�5t  Z   �� $��Center=�6e   $ 
Horizontal=�6�   .+�5u  ^   �� #��[ 6�   ��6h   �� $  ProductOption.Product.Name�6�   �� 5v  ^   #��[ 6�   ��6h   �� $ ProductOption.Size.Name�6�   5w  [   5x  W   �� 5y  V   $��1��6]   $��Center=�6m   ._�5z  Z   �� #��[ 6�   $��Center=�6�   ��6d   �� $ Quantity�$
 x{0}��6�   5{  W   �� 5|  V   $��2��6]   $��Center=�6m   ._�5}  Z   �� $��0��6d   $��Center=�6�   #��[ 6�   $��Center=�6�   ��6t   �� $ 
_totalLine�$ {0:N0}đ��6�   5~  W   5  S   5�  O   5�  K   5�  C   5�  ?   5�  ;   5�  7   5�  3   � 5�  2   $��7��67   $��Center=�6D    5�  6   �� 5�  :   $
��0.7* �6K   �� 5�  :   .+�5�  6   �� $��TRẠNG THÁI:��6@   #��[ 6S   $
��None{�6v   �� 5�  6   $��1��6@   #��[ 6s   ��6P   �� $ StatusString�6s   5�  3   �� 5�  2   -"   5        $ cancelOrder��5�  �   $��8��69   $��Hủy đơn��6F   $
 Hand��6X     GreenButton#��[ 6f   ���	#FF53825E$��0 10 0 0q�6�   ���#FFF6�   5�  /   5�  +   5�  '   5�  #   �� 5�  "   $��1��6)   ���	#FFF1F1F1$ 0 0 5 0��6P   ._�5�  &   �� .+�5�  *   �� -#   5        $ 	reloadBtn��5�  �   $��30��61   $��30��6<   ���	#FF53825E$ 5��6_   $��Right=�6p   $
��10 0q�6�   $
 Hand��6�   ._�5�  .   F� $��Center=�63   $��Center=�6N   
������������ z ��- ��o� �=Z @KL @KL ��� �r�*@��S � �  � � z �� ���[ ��[ � z ��- � z P%D � z �% אS� �7 ��� �� �PV� �7# �G�z��%@@�$ G�z��%@"��~j�@PV� ��K7�@ѐS� PG � z P%D ��� �� 	6z   5�  +   �� 5�  *   $	��Top=�6v   ��67   �� *ActualHeight+$
& tab3��$' OneWay=�.��5�  .   �� .+�5�  2   �� -$   5        $ 	userOrder��5�  ?   $��0 0 0 0q�6k   ��5�  6   �� 5�  :   .R�5�  >   �� -%   5        $ 15q�5�  E   $ 5��6R   ���#fff���#CCC$��1q�6�   $��50 10q�6�   $��Center=�6�   ._�5�  B   � $	��500��6G   $	��100��6S    5�  F   �� 5�  J   �� 5�  J   �� 5�  J    5�  F   �� 5�  J   �� 5�  J   �� 5�  J   .+�5�  F   �� $��Mã hóa đơn:��6P   #��[ 6c   6�   �� 5�  F   $��1��6t   #��[ 6�   ��6P   �� $ OrderHeaderID�6�   �� 5�  F   $��Đơn giá:��6P   #��[ 6`   $��1��6�   �� 5�  F   $��1��6�   $��1��6�   #��[ 6�   ��6P   �� $
 Cash�$ {0:N0}đ��6�   �� 5�  F   $��Thời gian đặt: ��6P   #��[ 6g   $��2��6�   �� 5�  F   $��2��6P   $��1��6]   #��[ 6�   ��6m   �� $ 	OrderDate�$ {0:hh\hmm d/M/yyyy}��6�   5�  C   5�  ?   5�  ;   5�  3   5�  /   5�  +   5�  '   5�  #   5�     5�     � 5�     -&   5        $
 tab4��5�  ,   $��1��6   $ 0��6:   .+�5�     �  5�  "   �� 5�  &   $
��0.8* �67   �� 5�  &   .+�5�  "   �� ���	#FF53825E$ 0 0 0 5��6@   �� 5�  "   $��1��6)   ���	#FFF1F1F15�  #   5�     � 5�     .+�5�  "   �� -'   5        $	��Top=�5�  )   $��30��6A   $��30��6L   ���	#FF53825E$ 5��6o   $��Right=�6�   $
��10 0q�6�   $
 Hand��6�   ._�5�  &   F� $��Center=�6+   $��Center=�6F   
������������ z ��- ��o� �=Z @KL @KL ��� �r�*@��S � �  � � z �� ���[ ��[ � z ��- � z P%D � z �% אS� �7 ��� �� �PV� �7# �G�z��%@@�$ G�z��%@"��~j�@PV� ��K7�@ѐS� PG � z P%D ��� �� 	6r   5�  #   �� 5�  "   $��Center=�6)   $��Center=�6D     5�  &     5�  *   $	! 245Q�6;   $
" #000��6K   $# 5Q�6X   $$ 25Q�6h   (   Opacity$	( 0.4Q�6x   ._�5�  &   �� -(   5        $
 card��5�  -   $	��500��6;   $	��300��6G   ���	#FFFCF0DA$ 20��6k   $��1q�6}   ���#DDD$ 20q�6�   ._�5�  *   �  5�  .   �� 5�  2   �� 5�  2    5�  .   �� 5�  2   $
��0.5* �6@   �� 5�  2   $
��0.8* �6@   �� 5�  2   �� 5�  2   .+�5�  .   �� $ 
Horizontal=�69   $��Center=�6R   .+�5�  2   �� $ 0q�69   ._�5�  6   �� $��/asset/logo/greenline.png��6<   5�  3   �� 5�  2   $��5 0 0 0q�6<   $��QUATITEA��6M   #��[ 6]   $��20(�6|   $ 0q�6�   $��Center=�6�   5�  /   �� 5�  .   $��1��65   $��Center=�6B   ._�5�  2   �� #��[ 6r   $��30(�6�   ��6<   �� $ Score�$ Điểm: {0}��6�   5�  /   �� 5�  .   $��0��65   $��1��6B   $��3��6R   ._�5�  2   �� -)   5        $ QRCode��5�  8   5�  /   �� 5�  .   $��3��69   $��Center=�6F   .+�5�  2   �� #��[ 6W   ��6<   �� $
 Name�6W   �� 5�  2   #��[ 6s   ���	#FF53825E��6<   �� $	 DOB�$ {0:d/M/yyyy}��6�   5�  /   5�  +   5�  '   5�  #   5�     5�     � 5�     -*   5        $ tabCart��5�  ,   $��1��6   $y�Hidden=�6=   ���	#FFF1F1F1$ 9��6h   .+�5�     �  5�  "   �� 5�  &   $��50 �64   �� 5�  &   $	��360 �64   �� 5�  &   .+�5�  "   �� -+   5        $��50��5�  )   $��50��64   $ 10q�6@   $	��Top=�6l   $
��Left=�6�   ._�5�  &   �� $��/asset/icons/back.png��6,   5   #   �� 5  "   $	��200��6)   $��Right=�65   $
 15 5q�6Q   $��0 5 0 5q�6`   $ 	10 0 0 10��6q   ���	#FF434948$��1q�6�   ._�5  &   �� $��Center=�61   $ 
Horizontal=�6L   .+�5  *   �� $��Điểm:��64   #��[ 6A   �� 5  *   -,   5        $ 	userSCore��5  4   $��5 0 0 0q�6G   #��[ 6X   ��6z   �� $ Score�5  '   5  #   �� 5  "   $��1��6-   $��
70 20 50 0q�6:   $��2��6N   .+�5  &   �� $��30��6-   $��0 0 20 0q�69   $��0 0 0 1q�6K   ���	#FF434948$ 0 0 0 10q�6}   ._�5	  *   � $��Bottom=�6/    5
  .   �� 5  2   $	��100 �6C   �� 5  2   $
��1.5* �6C   �� 5  2   $
��0.5* �6C   $	��150��6P   �� 5  2   �� 5  2   $
��0.2* �6C   $��80��6P   .+�5  .   �� $��2��65   $��Center=�6I   ._�5  2   �� $��Sản phẩm��6<   #��[ 6L   5  /   �� 5  .   $��2��65   $��Center=�6E   ._�5  2   �� $��Số lượng��6<   #��[ 6L   5  /   �� 5  .   $��3��65   $��Center=�6E   ._�5  2   �� $��Thành tiền��6<   #��[ 6N   5  /   �� 5  .   $��4��65   $��Center=�6E   ._�5  2   �� $
��Xóa��6<   #��[ 6G   5  /   5  +   5  '   �� 5  &   $	��400��63   .��5   *   �� .+�5!  .   �� --   5        $
 cart��5!  ;   $��0 0 0 0q�6b   ��5"  2   �� 5#  6   .R�5$  :   �� $	��100��6A   $��0 0 0 1q�6N   ���	#FF434948._�5%  >   �  5&  B   �� 5'  F   $	��100 �6W   �� 5(  F   $
��1.5* �6W   �� 5)  F   $
��0.5* �6W   $	��150��6d   �� 5*  F   �� 5+  F   $
��0.2* �6W   $��80��6d   .+�5-  B   �� $ 20q�6I   ._�5.  F   �� ��6L   �� $! ProductOption.Product.Image�  5/  J     50  N   $	! 245Q�6_   $" 	#00DADADA��6o   $# 1Q�6�   $$ 20Q�6�   52  G   53  C   �� 54  B   $��1��6I   ._�55  F   �� $��Center=�6Q   .+�56  J   �� #��[ 6�   $��25(�6�   ��6T   �� $  ProductOption.Product.Name�6�   �� 57  J    
 myFont#��[ 6�   ���	#FF53825E��6T   �� $ ProductOption.Size.Name�$ Size {0}��6�   58  G   59  C   �� 5:  B   $��2��6I   ._�5;  F   �  5<  J   �� 5=  N   �� 5>  N   �� 5?  N   .+�5A  J   �� $ 	minusCart��6�   -.   5        $��-��5A  Q     myDarkRoundBtn#��[ 6]   $��25��6�   $��25��6�   $��0q�6�   �� 5B  J   $ quantity��6�   -/   5        $��1��5B  R   #��[ 6b   {�.  $��0q�6�   $��Center=�6�   $��Center=�6�   ��6�   �� *
Quantity+63  �� 5C  J   $ plusCart��6�   -0   5        $��2��5C  Q   $��+��6a   #��[ 6m   $��25��6�   $��25��6�   $��0q�6�   6�   5D  G   5E  C   �� 5F  B   $��3��6I   ._�5G  F   �� $��0��6P   $��Center=�6�   #��[ 6�   $��Center=�6�   $��20(�6�   ��6`   �� $ 
_totalLine�$ {0:N0}đ��6�   5H  C   �� 5I  B   $��4��6I   ._�5J  F   �� -1   5        $��2��5J  M   $��30��6]   $��30��6h   $ 10��6�   $ 5q�6�   $
 Hand��6�   $��8q�6�   ��6t   Z� ._�5K  J   �� $��asset/icons/delete.png��6P   5L  G   5M  C   5N  ?   5O  ;   5P  7   5R  /   5S  +   5T  '   5U  #   �� 5V  "   $��
70 0 70 20q�6-   $��2��6A   $��Bottom=�6N   .+�5W  &   � $	��0 5q�6+    5X  *   �� 5Y  .   �� 5Z  .   �� 5[  .   .+�5]  *   �� $ 10q�61   $��10q�6>   $ 5��6J   $��1q�6[   ���	#FF434948._�5^  .   �  5_  2   �� 5`  6   �� 5a  6   .+�5c  2   �� #��[ 6<   $��Tổng tiền:��6^   $��Center=�6p   �� 5d  2   -2   5        $ txtTotal��5d  �   $��1��6<   #��[ 6L   $��Right=�6n   $	 0 5q�6�   $	��0đ��6�   6�   5e  /   5f  +   �� 5g  *   $ 10q�61   $��10q�6>   $��1��6J   $ 5��6Z   $��1q�6k   ���	#FF434948._�5h  .   �  5i  2   �� 5j  6   �� 5k  6   .+�5m  2   �� #��[ 6<   $��Tiền giảm:��6^   $��Center=�6p   �� 5n  2   -3   5        $ txtDiscount��5n  �   $��1��6<   #��[ 6L   $��Right=�6n   $	 0 5q�6�   $	��0đ��6�   6�   5o  /   5p  +   �� 5q  *   $ 10q�61   $��10q�6>   $��2��6J   $ 5��6Z   $��1q�6k   ���	#FF434948���	#FF434948._�5r  .   �  5s  2   �� 5t  6   �� 5u  6   .+�5w  2   �� #��[ 6<   
�������$��Thanh toán:��6q   $��Center=�6�   �� 5x  2   -4   5        $ txtCash��5x  �   $��1��6<   #��[ 6L   
�������$��Right=�6�   $	 0 5q�6�   $	��0đ��6�   6�   5y  /   5z  +   5|  '   � 5}  &   $
��10 5q�6+    5~  *   �� 5  .   �� 5�  .   .+�5�  *   �� $��Center=�65   $ 
Horizontal=�6P   .+�5�  .   �� #��[ 68   $��
Áp dụng��6Z   $��Center=�6i   �� 5�  .   -5   5        $ txtApplyScore��5�  8   #��[ 6O   $��Center=�6�   $��5 0 0 0q�6�   ��6q   �� $ Score�6�   �� 5�  .   #��[ 68   $��điểm��6Z   $��Center=�6f   $��5 0 0 0q�6�   5�  +   � 5�  *   $��1��6/   $��50��6?   $��20��6J   $
��Left=�6V   .+�5�  .   �� -6   5        $ ScoreApply_Bar��5�  �   $��50��65   $��10��6@   #��[  6L   $ 5��6m   ) J� Opacity$	) 0.5Q�6~   $��0,5,0,0q�6�   $	��Top=�6�   6�   �� 5�  .   -7   5        $ ScoreApply_Button��5�     #��[  65   $ 99��6V   $��20��6h   $��20��6s   6   5�  +   5�  '   � 5�  &    5�  *   �� 5�  .   �� 5�  .   �� 5�  .   .+�5�  *   �� -8   5        $ 
cancelCart��5�  m   ���#aaa$��10q�6C   $ 10q�6O   $ 5��6\   ._�5�  .   �� $��
Hủy bỏ��68   #��[ 6F   $��Center=�6k   $	 0 5q�6�   5�  +   �� 5�  *   -9   5        $ continuePay��5�  �   $��1��61   ���	#FF53825E$��10q�6X   $��2��6d   $ 10q�6x   $ 5��6�   ._�5�  .   �� $��Tiếp tục��68   #��[ 6H   $��Center=�6m   $	 0 5q�6�   5�  +   5�  '   5�  #   5�     5�     � 5�     -:   5        $ 
tabConfirm��5�  ,   $��1��6   $y�Hidden=�6@   ���	#FFF1F1F1$ 10��6k   .+�5�     �  5�  "   �� 5�  &   $��50 �64   �� 5�  &   .+�5�  "   �� -;   5        $��50��5�  )   $��50��64   $ 10q�6@   $	��Top=�6M   $
��Left=�6e   ._�5�  &   �� $��/asset/icons/back.png��6,   5�  #   � 5�  "   $��1��6'    5�  &   �� 5�  *   $��* �6;   �� 5�  *   .+�5�  &   �� $ 50q�6-   ._�5�  *   �� ���#FFF$ 5��6C   ._�5�  .   �� .+�5�  2   �� #��[ 6<   $��Center=�6[   $��Thông tin giao hàng��6x   � 5�  2    5�  6   �� 5�  :   �� 5�  :   �� 5�  :   .+�5�  6   �� .+�5�  :   �� $��Tên người nhận: ��6D   #��[ 6\   $��20 10q�6~   �� 5�  :   $
��20 0q�6A   $ 5q�6O   ���	#FF434948$��1q�6s   $ 5��6�   ._�5�  >   �� -<   5        $ receiverName��5�  j   #��[ 6F   6j   5�  ;   5�  7   �� 5�  6   $��1��6A   .+�5�  :   �� $��Địa chỉ nhận: ��6D   #��[ 6Z   $��20 10q�6|   �� 5�  :   $
��20 0q�6A   $ 5q�6O   ���	#FF434948$��1q�6s   $ 5��6�   ._�5�  >   �� -=   5        $ receiverAddress��5�  j   #��[ 6F   6j   5�  ;   5�  7   �� 5�  6   $��2��6A   .+�5�  :   �� $��Số điện thoại: ��6D   #��[ 6[   $��20 10q�6}   �� 5�  :   $
��20 0q�6A   $ 5q�6O   ���	#FF434948$��1q�6s   $ 5��6�   ._�5�  >   �� ->   5        $ receiverPhone��5�  j   #��[ 6F   6j   5�  ;   5�  7   5�  3   5�  /   5�  +   5�  '   �� 5�  &   $ 50q�6-   $��1��6:   ._�5�  *   �� ���#FFF$ 5��6C   $ 10q�6T   ._�5�  .   �� .+�5�  2   �� $
��auto��6?   $	��100��6M   $	��150��6]   $	��Top=�6m   .��5�  6   �� .+�5�  :   �� -?   5        $
 bill��5�  G   $��0 0 0 0q�6n   ��5�  >   �� 5�  B   .R�5�  F   �� $	 0 3q�6M   ._�5�  J   �  5�  N   �� 5�  R   $
��1.5* �6c   �� 5�  R   $
��0.5* �6c   �� 5�  R   .+�5�  N   �� $��0��6U   ._�5�  R   �� $��Center=�6]   $ 
Horizontal=�6x   .+�5�  V   �� #��[ 6�   ��6`   �� $  ProductOption.Product.Name�6�   �� 5�  V   #��[ 6�   ��6`   �� $ ProductOption.Size.Name�6�   5�  S   5�  O   �� 5�  N   $��1��6U   $��Center=�6e   ._�5�  R   �� #��[ 6�   $��Center=�6�   ��6\   �� $ Quantity�$
 x{0}��6�   5�  O   �� 5�  N   $��2��6U   $��Center=�6e   ._�5�  R   �� $��0��6\   $��Center=�6�   #��[ 6�   $��Center=�6�   ��6l   �� $ 
_totalLine�$ {0:N0}đ��6�   5�  O   5�  K   5�  G   5�  C   5�  ;   5�  7   5�  3   �� 5�  2   $ 10q�69   $��10q�6F   $ 5��6R   $��1q�6c   ���	#FF434948._�5�  6   �  5�  :   �� 5�  >   �� 5�  >   .+�5�  :   �� #��[ 6D   $��Tổng tiền:��6f   $��Center=�6x   �� 5�  :   -@   5        $ 	billTotal��5�  �   $��1��6D   #��[ 6T   $��Right=�6v   $	 0 5q�6�   $	��0đ��6�   6�   5�  7   5�  3   �� 5�  2   $ 10q�69   $��10q�6F   $ 5��6R   $��1q�6c   ���	#FF434948._�5�  6   �  5�  :   �� 5�  >   �� 5�  >   .+�5�  :   �� #��[ 6D   $��Tổng tiền:��6f   $��Center=�6x   �� 5�  :   -A   5        $ billDiscount��5�  �   $��1��6D   #��[ 6T   $��Right=�6v   $	 0 5q�6�   $	��0đ��6�   6�   5�  7   5   3   �� 5  2   $ 10q�69   $��10q�6F   $ 5��6R   $��1q�6c   ���	#FF434948���	#FF434948._�5  6   �  5  :   �� 5  >   �� 5  >   .+�5  :   �� #��[ 6D   
�������$��Thanh toán:��6y   $��Center=�6�   �� 5  :   -B   5        $ billCash��5  �   $��1��6D   #��[ 6T   
�������$��Right=�6�   $	 0 5q�6�   $	��0đ��6�   6�   5	  7   5
  3   �� 5  2   -C   5        $ 
confirmBtn��5  z   ���	#FF53825E$��10q�6P   $ 10q�6\   $ 5��6i   $
 Hand��6�   ._�5  6   �� $��Xác nhận��6@   #��[ 6P   $��Center=�6u   $	 0 5q�6�   5  3   5  /   5  +   5  '   5  #   5     5     5     5     5     5     5     5     