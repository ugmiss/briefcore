
```
1. WM_NULL=0X 0000:
2. WM_CREATE=0X 0001:    应用程序创建一个窗口
3. WM_DESTROY=0X 0002:    一个窗口被销毁
4. WM_MOVE=0X 0003: 移动一个窗口
5. WM_SIZE=0X 0005:改变一个窗口的大小
6. WM_ACTIVATE=0X 0006: 一个窗口被激活或失去激活状态；
7. WM_SETFOCUS=0X 0007: 获得焦点后
8. WM_KILLFOCUS=0X 0008: 失去焦点
9. WM_ENABLE=0X 000A: 改变enable状态
10. WM_SETREDRAW=0X 000B: 设置窗口是否能重画
11. WM_SETTEXT=0X 000C: 应用程序发送此消息来设置一个窗口的文本
12. WM_GETTEXT=0X 000D: 应用程序发送此消息来复制对应窗口的文本到缓冲区
13. WM_GETTEXTLENGTH=0X 000E: 得到与一个窗口有关的文本的长度（不包含空字符）
14. WM_PAINT=0X 000F: 要求一个窗口重画自己
15. WM_CLOSE=0X 0010: 当一个窗口或应用程序要关闭时发送一个信号
16. WM_QUERYENDSESSION=0X 0011: 当用户选择结束对话框或程序自己调用ExitWindows函数
17. WM_QUIT=0X 0012: 用来结束程序运行或当程序调用postquitmessage函数
18. WM_QUERYOPEN=0X 0013: 当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
19. WM_ERASEBKGND=0X 0014: 当窗口背景必须被擦除时（例在窗口改变大小时）
20. WM_SYSCOLORCHANGE=0X 0015: 当系统颜色改变时，发送此消息给所有顶级窗口
21. WM_ENDSESSION=0X 0016:当系统进程发出WM_QUERYENDSESSION消息后,此消息发送给应用程序,通知它对话是否结束
22. WM_SYSTEMERROR=0X 0017: 
23. WM_SHOWWINDOW=0X 0018: 当隐藏或显示窗口是发送此消息给这个窗口
24. WM_ACTIVATEAPP=0X 001C: 发此消息给应用程序哪个窗口是激活的，哪个是非激活的；
25. WM_FONTCHANGE=0X 001D: 当系统的字体资源库变化时发送此消息给所有顶级窗口
26. WM_TIMECHANGE=0X 001E: 当系统的时间变化时发送此消息给所有顶级窗口
27. WM_CANCELMODE=0X 001F: 发送此消息来取消某种正在进行的摸态（操作）
28. WM_SETCURSOR=0X 0020:  如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口
29. WM_MOUSEACTIVATE=0X 0021: 当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口
30. WM_CHILDACTIVATE=0X 0022: 发送此消息给MDI子窗口当用户点击此窗口的标题栏， 或当窗口被激活，移动，改变大小
31. WM_QUEUESYNC=0X 0023:  此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息
32. WM_GETMINMAXINFO=0X 0024: 此消息发送给窗口当它将要改变大小或位置；
33. WM_PAINTICON=0X 0026:  发送给最小化窗口当它图标将要被重画
34. WM_ICONERASEBKGND=0X 0027:此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画
35. WM_NEXTDLGCTL=0X 0028: 发送此消息给一个对话框程序去更改焦点位置
36. WM_SPOOLERSTATUS=0X 002A: 每当打印管理列队增加或减少一条作业时发出此消息
37. WM_DRAWITEM=0X 002B:  当button，combobox，listbox，menu的可视外观改变时发送此消息给这些空件的所有者
38. WM_MEASUREITEM=0X 002C: 当button, combobox, listbox, listviewcontrol, ormenuitem被创建时发送此消息 给控件的所有者
39. WM_DELETEITEM =0X 002D: 当thelistbox或combobox被销毁或当某些项被删除通过LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT消息
40. WM_VKEYTOITEM=0X 002E: 此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息
41. WM_CHARTOITEM=0X 002F:   此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息
42. WM_SETFONT=0X 0030:  当绘制文本时程序发送此消息得到控件要用的颜色
43. WM_GETFONT=0X 0031:  应用程序发送此消息得到当前控件绘制文本的字体
44. WM_SETHOTKEY=0X 0032:  应用程序发送此消息让一个窗口与一个热键相关连
45. WM_GETHOTKEY=0X 0033:  应用程序发送此消息来判断热键与某个窗口是否有关联
46. WM_QUERYDRAGICON =0X 0037: 此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
47. WM_COMPAREITEM=0X 0039: 发送此消息来判定combobox或listbox新增加的项的相对位置
48. WM_GETOBJECT=0X 003D:  
49. WM_COMPACTING=0X 0041: 显示内存已经很少了
50. WM_WINDOWPOSCHANGING=0X 0046: 发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数
51. WM_WINDOWPOSCHANGED=0X 0047: 发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数
52. WM_POWER=0X 0048:  (适用于16位的windows） 当系统将要进入暂停状态时发送此消息
53. WM_COPYDATA=0X 004A:  当一个应用程序传递数据给另一个应用程序时发送此消息
54. WM_CANCELJOURNAL=0X 004B: 当某个用户取消程序日志激活状态，提交此消息给程序
55. WM_NOTIFY=0X 004E:   当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口
56. WM_INPUTLANGCHANGEREQUEST=0X 0050: 当用户选择某种输入语言，或输入语言的热键改变
57. WM_INPUTLANGCHANGE=0X 0051: 当平台现场已经被改变后发送此消息给受影响的最顶级窗口
58. WM_TCARD=0X 0052:  当程序已经初始化windows帮助例程时发送此消息给应用程序
59. WM_HELP=0X 0053: 此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单,否则就发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口
60. WM_USERCHANGED=0X 0054: 当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息；
61. WM_NOTIFYFORMAT=0X 0055: 公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构在WM_NOTIFY消息，使用此控件能使某个控件与它的父控件之间进行相互通信
62. WM_CONTEXTMENU=0X 007B: 当用户某个窗口中点击了一下右键就发送此消息给这个窗口
63. WM_STYLECHANGING=0X 007C: 当调用SETWINDOWLONG函数将要改变一个或多个窗口的风格时发送此消息给那个窗口
64. WM_STYLECHANGED=0X 007D: 当调用SETWINDOWLONG函数一个或多个窗口的风格后发送此消息给那个窗口
65. WM_DISPLAYCHANGE=0X 007E: 当显示器的分辨率改变后发送此消息给所有的窗口
66. WM_GETICON=0X 007F:  此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄；
67. WM_SETICON=0X 0080:  程序发送此消息让一个新的大图标或小图与某个窗口关联；
68. WM_NCCREATE=0X 0081:  当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送；
69. WM_NCDESTROY=0X 0082:  此消息通知某个窗口，非客户区正在销毁
70. WM_NCCALCSIZE=0X 0083: 当某个窗口的客户区域必须被核算时发送此消息
71. WM_NCHITTEST=0X 0084:   移动鼠标，按住或释放鼠标时发生
72. WM_NCPAINT=0X 0085:  程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时；
73. WM_NCACTIVATE=0X 0086: 此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态；
74. WM_GETDLGCODE=0X 0087:   发送此消息给某个与对话框程序关联的控件， windows控制方位键和TAB键使输入进入 此控件通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它
75. WM_NCMOUSEMOVE=0X 00A0: 当光标在一个窗口的非客户区内移动时发送此消息给这个窗口,非客户区为：窗体的标题栏及窗的边框体
76. WM_NCLBUTTONDOWN=0X 00A1: 当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
77. WM_NCLBUTTONUP=0X 00A2: 当用户释放鼠标左键同时光标某个窗口在非客户区时发送此消息；
78. WM_NCLBUTTONDBLCLK=0X 00A3:当用户双击鼠标左键同时光标某个窗口在非客户区时发送此消息
79. WM_NCRBUTTONDOWN=0X 00A4: 当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息
80. WM_NCRBUTTONUP=0X 00A5: 当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息
81. WM_NCRBUTTONDBLCLK=0X 00A6:当用户双击鼠标右键同时光标某个窗口在非客户区时发送此消息
82. WM_NCMBUTTONDOWN=0X 00A7: 当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息
83. WM_NCMBUTTONUP=0X 00A8: 当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息
84. WM_NCMBUTTONDBLCLK=0X 00A9:当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息
85. WM_KEYFIRST=0X 0100: 
86. WM_KEYDOWN=0X 0100: 按下一个键
87. WM_KEYUP=0X 0101:  释放一个键
88. WM_CHAR=0X 0102:  按下某键，并已发出WM_KEYDOWN，WM_KEYUP消息
89. WM_DEADCHAR=0X 0103: 当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口
90. WM_SYSKEYDOWN=0X 0104:当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口；
91. WM_SYSKEYUP=0X 0105: 当用户释放一个键同时ALT键还按着时提交此消息给拥有焦点的窗口
92. WM_SYSCHAR=0X 0106: 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口
93. WM_SYSDEADCHAR=0X 0107: 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口
94. WM_INITDIALOG=0X 0110: 在一个对话框程序被显示前发送此消息给它,常用此消息初始化控件和执行其它任务
95. WM_COMMAND=0X 0111:  当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译
96. WM_SYSCOMMAND=0X 0112: 当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息
97. WM_TIMER=0X 0113:      发生了定时器事件
98. WM_HSCROLL=0X 0114:  当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件
99. WM_VSCROLL=0X 0115:  当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件
100. WM_INITMENU=0X 0116: 当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
101. WM_INITMENUPOPUP=0X 0117: 当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部
102. WM_MENUSELECT=0X 011F: 当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）
103. WM_MENUCHAR=0X 0120:  当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者；
104. WM_ENTERIDLE=0X 0121:  当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理一条或几条先前的消息后没有消息它的列队中等待
105. WM_MENURBUTTONUP=0X 0122: 
106. WM_MENUDRAG=0X 0123: 
107. WM_MENUGETOBJECT=0X 0124: 
108. WM_UNINITMENUPOPUP=0X 0125:
109. WM_MENUCOMMAND=0X 0126: 
110. WM_CHANGEUISTATE=0X 0127:
111. WM_UPDATEUISTATE=0X 0128:
112. WM_QUERYUISTATE=0X 0129:
113. WM_CTLCOLORMSGBOX=0X 0132: 在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
114. WM_CTLCOLOREDIT=0X 0133: 当一个编辑型控件将要被绘制时发送此消息给它的父窗口:通过响应这条消息,所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
115. WM_CTLCOLORLISTBOX=0X 0134:当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色
116. WM_CTLCOLORBTN=0X 0135: 当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
117. WM_CTLCOLORDLG=0X 0136: 当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
118. WM_CTLCOLORSCROLLBAR=0X 0137: 当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
119. WM_CTLCOLORSTATIC=0X 0138: 当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
120. WM_MOUSEMOVE=0X 0200:     移动鼠标
121. WM_LBUTTONDOWN=0X 0201    按下鼠标左键
122. WM_LBUTTONUP=0X 0202:    释放鼠标左键
123. WM_LBUTTONDBLCLK=0X 0203:     双击鼠标左键
124. WM_RBUTTONDOWN=0X 0204:   按下鼠标右键
125. WM_RBUTTONUP=0X 0205:    释放鼠标右键
126. WM_RBUTTONDBLCLK=0X 0206:   双击鼠标右键
127. WM_MBUTTONDOWN=0X 0207:   按下鼠标中键
128. WM_MBUTTONUP=0X 0208:    释放鼠标中键
129. WM_MBUTTONDBLCLK=0X 0209:   双击鼠标中键
130. WM_MOUSEWHEEL=0X 020A:  当鼠标轮子转动时发送此消息个当前有焦点的控件
131. WM_PARENTNOTIFY=0X 0210: 当MDI子窗口被创建或被销毁，或用户按了一下鼠标键而光标在子窗口上时发送此消息给它的父窗口
132. WM_ENTERMENULOOP=0X 0211: 发送此消息通知应用程序的主窗口已经进入了菜单循环模式
133. WM_EXITMENULOOP=0X 0212: 发送此消息通知应用程序的主窗口已退出了菜单循环模式
134. WM_NEXTMENU=0X 0213:
135. WM_SIZING=532:   当用户正在调整窗口大小时发送此消息给窗口；通过此消息应用程序可以监视窗口大小和位置也可以修改他们
136. WM_CAPTURECHANGED=533: 发送此消息给窗口当它失去捕获的鼠标时；
137. WM_MOVING=534:   当用户在移动窗口时发送此消息，通过此消息应用程序可以监视窗口大小和位置也可以修改他们；
138. WM_POWERBROADCAST=536: 此消息发送给应用程序来通知它有关电源管理事件；
139. WM_DEVICECHANGE=537:  当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序
140. WM_IME_STARTCOMPOSITION=0X 010D: 
141. WM_IME_ENDCOMPOSITION=0X 010E:
142. WM_IME_COMPOSITION=0X 010F:
143. WM_IME_KEYLAST=0X 010F:
144. WM_IME_SETCONTEXT=0X 0281:
145. WM_IME_NOTIFY=0X 0282:
146. WM_IME_CONTROL=0X 0283:
147. WM_IME_COMPOSITIONFULL=0X 0284:
148. WM_IME_SELECT=0X 0285:
149. WM_IME_CHAR=0X 0286:
150. WM_IME_REQUEST=0X 0288:
151. WM_IME_KEYDOWN=0X 0290:
152. WM_IME_KEYUP=0X 0291:
153. WM_MDICREATE=0X 0220:  应用程序发送此消息给多文档的客户窗口来创建一个MDI子窗口
154. WM_MDIDESTROY=0X 0221: 应用程序发送此消息给多文档的客户窗口来关闭一个MDI子窗口
155. WM_MDIACTIVATE=0X 0222: 应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到此消息后，它发出WM_MDIACTIVE消息给MDI子窗口（未激活）激活它；
156. WM_MDIRESTORE=0X 0223: 程序发送此消息给MDI客户窗口让子窗口从最大最小化恢复293. 到原来大小
157. WM_MDINEXT=0X 0224:  程序发送此消息给MDI客户窗口激活下一个或前一个窗口
158. WM_MDIMAXIMIZE=0X 0225: 程序发送此消息给MDI客户窗口来最大化一个MDI子窗口；
159. WM_MDITILE=0X 0226:  程序发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口
160. WM_MDICASCADE=0X 0227: 程序发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口
161. WM_MDIICONARRANGE=0X 0228: 程序发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口
162. WM_MDIGETACTIVE=0X 0229:    程序发送此消息给MDI客户窗口来找到激活的子窗口的句柄
163. WM_MDISETMENU=0X 0230:  程序发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单
164. WM_ENTERSIZEMOVE=0X 0231: 
165. WM_EXITSIZEMOVE=0X 0232:
166. WM_DROPFILES=0X 0233:
167. WM_MDIREFRESHMENU=0X 0234:
168. WM_MOUSEHOVER=0X 02A1:
169. WM_MOUSELEAVE=0X 02A3:
170. WM_CUT=0X 0300:    程序发送此消息给一个编辑框或combobox来删除当前选择的文本
171. WM_COPY=0X 0301:   程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板
172. WM_PASTE=0X 0302:   程序发送此消息给editcontrol或combobox从剪贴板中得到数据
173. WM_CLEAR=0X 0303:   程序发送此消息给editcontrol或combobox清除当前选择的内容；
174. WM_UNDO=0X 0304:   程序发送此消息给editcontrol或combobox撤消最后一次操作
175. WM_RENDERFORMAT=0X 0305；
176. WM_RENDERALLFORMATS=0X 0306:
177. WM_DESTROYCLIPBOARD=0X 0307: 当调用ENPTYCLIPBOARD函数时发送此消息给剪贴板的所有者
178. WM_DRAWCLIPBOARD=0X 0308:  当剪贴板的内容变化时发送此消息给剪贴板观察链的第一个窗口；它允许用剪贴板观察窗口来显示剪贴板的新内容；
179. WM_PAINTCLIPBOARD=0X 0309:  当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区需要重画；
180. WM_VSCROLLCLIPBOARD=0X 030A:
181. WM_SIZECLIPBOARD=0X 030B:  当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区域的大小已经改变是此消息通过剪贴板观察窗口发送给剪贴板的所有者；
182. WM_ASKCBFORMATNAME=0X 030C:  通过剪贴板观察窗口发送此消息给剪贴板的所有者来请求一个CF_OWNERDISPLAY格式的剪贴板的名字
183. WM_CHANGECBCHAIN=0X 030D:  当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链的第一个窗口；
184. WM_HSCROLLCLIPBOARD =0X 030E: 此消息通过一个剪贴板观察窗口发送给剪贴板的所有者；它发生在当剪贴板包 CFOWNERDISPALY格式的数据并且有个事件在剪贴板观察窗的水平滚动条上；所有者应滚动剪贴板图象并更新滚动条的值；
185. WM_QUERYNEWPALETTE=0X 030F:  此消息发送给将要收到焦点的窗口，此消息能使窗口在收到焦点时同时有机会实 现他的逻辑调色板
186. WM_PALETTEISCHANGING=0X 0310:    当一个应用程序正要实现它的逻辑调色板时发此消息通知所有的应用程序
187. WM_PALETTECHANGED=0X 0311:  此消息在一个拥有焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此来改变系统调色板
188. WM_HOTKEY=0X 0312:    当用户按下由REGISTERHOTKEY函数注册的热键时提交此消息
189. WM_PRINT=791:    应用程序发送此消息仅当WINDOWS或其它应用程序发出一个请求要求绘制一个应用程序的一部分；
190. WM_PRINTCLIENT=792: 
191. WM_HANDHELDFIRST=856:
192. WM_HANDHELDLAST=863:
193. WM_PENWINFIRST=0X 0380:
194. WM_PENWINLAST=0X 038F:
195. WM_COALESCE_FIRST=0X 0390:
196. WM_COALESCE_LAST=0X 039F:
197. WM_DDE_FIRST=0X 03E0:
198. WM_DDE_INITIATE=WM_DDE_FIRST+0:  一个DDE客户程序提交此消息开始一个与服务器程序的会话来响应那个指定的程序和主题名；
199. WM_DDE_TERMINATE=WM_DDE_FIRST+1: 一个DDE应用程序(无论是客户还是服务器)提交此消息来终止一个会话；
200. WM_DDE_ADVISE=WM_DDE_FIRST+2:  一个DDE客户程序提交此消息给一个DDE服务程序来请求服务器每当数据项改变时更新它
201. WM_DDE_UNADVISE=WM_DDE_FIRST+3:  一个DDE客户程序通过此消息通知一个DDE服务程序不更新指定的项或一个特殊的剪贴板格式的项
202. WM_DDE_ACK =WM_DDE_FIRST+4:  此消息通知一个DDE（动态数据交换）程序已收到并正在处理WM_DDE_POKE, WM_DDE_EXECUTE, WM_DDE_DATA, WM_DDE_ADVISE, WM_DDE_UNADVISE, or WM_DDE_INITIAT 消息. 
203. WM_DDE_DATA=WM_DDE_FIRST+5:   一个DDE服务程序提交此消息给DDE客户程序来传递个一数据项给客户或通知客户的一条可用数据项
204. WM_DDE_REQUEST=WM_DDE_FIRST+6: 一个DDE客户程序提交此消息给一个DDE服务程序来请求一个数据项的值；
205. WM_DDE_POKE =WM_DDE_FIRST+7:  一个DDE客户程序提交此消息给一个DDE服务程序，客户使用此消息来请求服务器接收一个未经同意的数据项；服务器通过答复WM_DDE_ACK消息提示是否它接收这个数据项；
206. WM_DDE_EXECUTE=WM_DDE_FIRST+8: 一个DDE客户程序提交此消息给一DDE服务程序来发送一个字符串给服务器让它象串行命令一样被处理,服务器通过提交WM_DDE_ACK消息来作回应；
207. WM_DDE_LAST=WM_DDE_FIRST+8:  
208. WM_APP=0X 8000:
209. WM_USER=0X 0400: 此消息能帮助应用程序自定义私有消息；通知消息(Notificationmessage)是指这样一种消息，一个窗口内的子控件发生了一些事情，需要通知父窗口。通知消息只适用于标准的窗口控件如按钮、列表框、组合框、编辑框，以及Windows95公共控件如树状视图、列表视图等。例如，单击或双击一个控件、在控件中选择部分文本、操作控件的滚动条都会产生通知消息。
210. BN_CLICKED  : 用户单击了按钮
211. BN_DISABLE  : 按钮被禁止
212. BN_DOUBLECLICKED : 用户双击了按钮
213. BN_HILITE: 用户加亮了按钮
214. BN_PAINT 按钮应当重画
215. BN_UNHILITE  加亮应当去掉组合框
216. CBN_CLOSEUP  组合框的列表框被关闭
217. CBN_DBLCLK  用户双击了一个字符串
218. CBN_DROPDOWN 组合框的列表框被拉出
219. CBN_EDITCHANGE 用户修改了编辑框中的文本
220. CBN_EDITUPDATE 编辑框内的文本即将更新
221. CBN_ERRSPACE 组合框内存不足
222. CBN_KILLFOCUS 组合框失去输入焦点
223. CBN_SELCHANGE 在组合框中选择了一项
224. CBN_SELENDCANCEL 用户的选择应当被取消
225. CBN_SELENDOK  用户的选择是合法的
226. CBN_SETFOCUS  组合框获得输入焦点编辑框
227. EN_CHANGE   编辑框中的文本己更新
228. EN_ERRSPACE   编辑框内存不足
229. EN_HSCROLL   用户点击了水平滚动条
230. EN_KILLFOCUS  编辑框正在失去输入焦点
231. EN_MAXTEXT   插入的内容被截断
232. EN_SETFOCUS   编辑框获得输入焦点
233. EN_UPDATE   编辑框中的文本将要更新
234. EN_VSCROLL   用户点击了垂直滚动条消息含义列表框
235. LBN_DBLCLK   用户双击了一项
236. LBN_ERRSPACE  列表框内存不够
237. LBN_KILLFOCUS  列表框正在失去输入焦点
238. LBN_SELCANCEL  选择被取消
239. LBN_SELCHANGE  选择了另一项
240. LBN_SETFOCUS  列表框获得输入焦点
```