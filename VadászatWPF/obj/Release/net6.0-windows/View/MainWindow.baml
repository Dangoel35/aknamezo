   M S B A M L   `   `   `  ����   VadászatWPF    VadászatWPF.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$
  Akna��$
��1000��$
��1000�� J� InputBindings �� 
 �� Key$ Right�� �� CommandParameter$ right�� �� Command �� *MoveCommand+�� $
 Left��$
 left�� �� *MoveCommand+�� $ Up��$ up�� �� *MoveCommand+�� $
 Down��$
 down�� �� *MoveCommand+�� $	 Tab�� �� * SwitchSelectedCharacterCommand+.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�o� $��0��.@�m� $
��Menu��.@�m� $��Mentés�� m� Command �� *SaveGameCommand+m� $��
Betöltés�� �� *LoadGameCommand+m� $��Pályaméret��.@�m� $	��6x6�� �� *
Newgame6+m� $��10x10�� �� *	Newgame10+m� $��16x16�� �� *	Newgame16+�� $��1������ *Fields+���� .R�A�  A� Rows �� *	MapSize+ A� Columns �� *	MapSize+���� .R��� ���� *ButtonClick+���� *XY+���� *Text+���� 	 �� 
TargetType"	 ��
 �� Triggers
 ��  �� Binding �� *
IsHidden+ �� Value 1.a���  �� Property � �� �� Value
 �   ���  �� *
IsHidden+ 2.a���  � ��
 �������  �� *
IsHidden+ 3.a���  � ��
 �����