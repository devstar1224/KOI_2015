   M S B A M L   `   `   `  ����  MSK Project    MSK_Project.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          � Title$  
MainWindow��5       � WindowStyle$
 None=�60   
������  � AllowsTransparency . $��760.554��6�   $��563.754��6�    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�   .��5      � $��	0,0,0,256q�6   $
��Left=�6   $	��521��69   ��5   
   �� 5	       �� ImageSource$3 -Resource/제목-없음-1_0013_레이어-3.png��6   .+�5   
   �� -   5        $��	ListView1��5       J� Opacity$	 0.5Q�6$   $
��Left=�6�   $	��178��6�   $	��Top=�6�   $	��503��6�   $��	9,269,0,0q�6�   5      �� 5   
   -   5        $
��img1��5      $
��Left=�6   $��Resource/default.png��67   $	��118��6U   $	��Top=�6b   $	��124��6z   $��	27,51,0,0q�6�   $
��Fill=�6�    J� OpacityMask 5      �� 5      $ Resource/default.png��6    J� Clip 5      @� 5      	 @� Center$	 	82.5,88.5&�6"   
 @� RadiusX$	
 120Q�65    @� RadiusY$	 100Q�6C    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 3Q�6#      	Direction$	 330Q�63      Color$ White��6C      Opacity$	 0.7Q�6Q      
BlurRadius$	 100Q�6_   5      �� 5   
   -   5        $��statuss��5   �   $
��Left=�6�   $��177,162,0,0q�6�   $	��Top=�6�   $	��335��6�   $��18��6�   �� 5   
   -   5        $��volumes��5   5   $
��Left=�6D   $��427,133,0,0q�6_   $	��Top=�6t   $��85��6�    � Maximum$	 100Q�6�    � Value$ 80Q�6�   $��23��6�   �� 5    
   -   5        $��mname��5       $��제목:��6   $
��Left=�6+   $��
163,65,0,0q�6F   $	��Top=�6Z   $��25(�6r   
�������$	��349��6�   �� 5!   
   -   5        $��gname��5!      $��Genre:��6   $
��Left=�6.   $��177,120,0,0q�6I   $	��Top=�6^   $��18(�6v   
�������6�   �� 5"   
   -   5         ��Name$ imgrsrc��5"      $
��Left=�6!   $��Resource/default.png��6<   $	��144��6Z   $	��Top=�6g   $	��146��6   $��592,192,-182,0q�6�    5#      �� 5$      $ Resource/default.png��6    5&        5'      $ 4Q�6#   $	 330Q�63   $ White��6C   $	 0.7Q�6Q   $ 4Q�6_   5*      �� 5+   
   -	   5        $ 
defaultimg��5+      $	 0.5Q�6�   $
��Left=�6�   $��Resource/반복2.png��6�   $��61��6�   $	��Top=�6�   $��78��6�   $��
26,185,0,0q�6�    J� RenderTransformOrigin$ 	0.5,1.262&�6	   5-        5.      $ 4Q�6#   $	 330Q�63   $ White��6C   $	 0.7Q�6Q   $ 4Q�6_   51      �� 52   
   -
   5        $ m_loop��52      $
��Left=�6    $��Resource/반복1.png��6;   $��30��6U   $	��Top=�6a   $��30��6y   $��664,192,-136,0q�6�    53      �� 54      $ Resource/반복2.png��6    57        58      $ 4Q�6#   $	 330Q�63   $ White��6C   $	 0.7Q�6Q   $ 4Q�6_   5;      �� 5<   
   -   5        $ a_loop��5<      $
��Left=�6    $��Resource/반복3.png��6;   $��30��6U   $	��Top=�6a   $��30��6y   $��581,216,-53,0q�6�    5>        5?      $ 4Q�6#   $	 330Q�63   $ White��6C   $	 0.7Q�6Q   $ 4Q�6_   5B      �� 5C   
   -   5        $ d_img��5C      $	 0.5Q�6~   $
��Left=�6�   $��Resource/반복2.png��6�   $��61��6�   $	��Top=�6�   $��78��6�   $��616,185,-136,0q�6�    5E        5F      $ 4Q�6#   $	 330Q�63   $ White��6C   $	 0.7Q�6Q   $ 4Q�6_   5I      �� 5K   
   -   5        $
��Left=�5K   /   $��25��6J   $	��Top=�6V   $	��564��6n   $��0,1,0,0q�6z   $��Resource/위.png��6�   $
��Fill=�6�   �� 5L   
   $
��Left=�6   $	��100��6+   $��
53,-10,0,0q�68   $	��Top=�6L   $	��100��6d   �� 5M   
   $
��Left=�6   $��5��6+   $��0,26,0,0q�66   $	��Top=�6H   $
��2000��6`   $��Resource/위2.png��6m   $��UniformToFill=�6�   �� 5N   
   -   5        $��playb��5N      $
��Left=�6w   $��79��6�   $��186,185,0,0q�6�   $	��Top=�6�   $��77��6�   $��Resource/시작.png��6�   �� 5O   
   -   5        $��waitb��5O      $
��Left=�6v   $��79��6�   $��268,185,0,0q�6�   $	��Top=�6�   $��77��6�   $��Resource/멈추기2.png��6�   �� 5P   
   -   5        $��stopb��5P      $
��Left=�6v   $��79��6�   $��350,185,0,0q�6�   $	��Top=�6�   $��77��6�   $��Resource/멈추기.png��6�   �� 5Q   
   -   5        $��leftb��5Q      $
��Left=�6u   $��79��6�   $��432,185,0,0q�6�   $	��Top=�6�   $��77��6�   $)��#Resource/오른쪽 넘어가기.png��6�   �� 5R   
   -   5        $��rightb��5R      $
��Left=�6y   $��79��6�   $��104,185,0,0q�6�   $	��Top=�6�   $��77��6�   $&�� Resource/왼쪽 넘어가기.png��6�   �� 5S   
   -   5        $��exit1��5S      $
��Left=�6w   $��20��6�   $��	480,1,0,0q�6�   $	��Top=�6�   $��41��6�   $��Resource/Exit.png��6�   �� 5T   
   -   5        $��추가��5T      $
��Left=�63   $��
10,452,0,0q�6N   $	��Top=�6b   $��61��6z   �� 5U   
   -   5        $��제거��5U      $
��Left=�65   $��
76,452,0,0q�6P   $	��Top=�6d   $��61��6|   �� 5V   
   -   5        $��EQ��5V      $
��Left=�6   $��142,452,0,0q�69   $	��Top=�6N   $��61��6f   6q   �� 5W   
   -   5        $��	숨기기��5W      $
��Left=�6   $��208,452,0,0q�6:   $	��Top=�6O   $��61��6g   6r   �� 5X   
   -   5        $��장르분석��5X      $
��Left=�6    $��274,452,0,0q�6;   $	��Top=�6P   $��61��6h   6s   5\      5^      