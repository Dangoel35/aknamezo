   M S B A M L   `   `   `  ����  VadászatWPF    VadászatWPF.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$
  Akna��5      $
��1000��6   $
��1000��6(   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   .@�5      m� $
��Menu��6   .@�5      m� $��Mentés��6    m� Command 6+   �� *SaveGameCommand+6Q   m� 5      $��
Betöltés��6    6-   �� *LoadGameCommand+6S   m� 5      $��Pályaméret��6   .@�5      m� $	��6x6��6    6,   �� *
Newgame6+6K   m� 5      $��10x10��6    6.   �� *	Newgame10+6N   m� 5      $��16x16��6#    62   �� *	Newgame16+6R   5      5      5      �� 5   
   $��1��6   ��6$   �� *Fields+��5      �� 5      .R�5      A�  A� Rows 6"   �� *	MapSize+ A� Columns 6;   �� *	MapSize+5       ��5"      �� 5$      .R�5%      �� ��6   �� *ButtonClick+��6=   �� *XY+��6]   �� *Text+��5(      �� 5*       �� 
TargetType" ��6$    �� Triggers 5+   "   �� 5-   &    �� Binding 62   �� *
IsHidden+ �� Value 16O   .a�5.   *   �� 	 �� Property	 � ��
 �� Value

 �   �6G   5/   '   �� 50   &    62   �� *
IsHidden+ 26O   .a�51   *   �� 	 � ��

 �����6G   52   '   �� 53   &    62   �� *
IsHidden+ 36O   .a�54   *   �� 	 � ��

 �����6G   55   '   57      59      5:      5<      5?      5@      