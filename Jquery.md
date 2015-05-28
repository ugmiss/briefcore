### 选择器nth-child ###
```
第一：nth-child(number)
//直接匹配第number个元素。参数number必须为大于0的整数。
(EG) li:nth-child(3){background:orange;}/*把第3个LI的背景设为橙色*/ 
第二：nth-child(an) 
//匹配所有倍数为a的元素。其中参数an中的字母n不可缺省，它是倍数写法的标志，如3n、5n。 
(EG) li:nth-child(3n){background:orange;}/*把第3、第6、第9、…、所有3的倍数的LI的背景设为橙色*/ 
第三：nth-child(an+b) 与 :nth-child(an-b) 
//先对元素进行分组，每组有a个，b为组内成员的序号，其中字母n和加号+不可缺省，位置不可调换，这是该写法的标志，其中a,b均为正整数或0。
如3n+1、5n+1。但加号可以变为负号，此时匹配组内的第a-b个。（其实an前面也可以是负号，但留给下一部分讲。） 
(EG)li:nth-child(3n+1){background:orange;}/*匹配第1、第4、第7、…、每3个为一组的第1个LI*/ 
li:nth-child(3n+5){background:orange;}/*匹配第5、第8、第11、…、从第5个开始每3个为一组的第1个LI*/ 
li:nth-child(5n-1){background:orange;}/*匹配第5-1=4、第10-1=9、…、第5的倍数减1个LI*/ 
li:nth-child(3n±0){background:orange;}/*相当于(3n)*/ 
li:nth-child(±0n+3){background:orange;}/*相当于(3)*/ 
第四：nth-child(-an+b) 
//此处一负一正，均不可缺省，否则无意义。这时与:nth-child(an+1)相似，都是匹配第1个，但不同的是它是倒着算的，
//从第b个开始往回算，所以它所匹配的最多也不会超过b个。 
(EG) li:nth-child(-3n+8){background:orange;}/*匹配第8、第5和第2个LI*/ 
li:nth-child(-1n+8){background:orange;}/*或(-n+8)，匹配前8个（包括第8个）LI，这个较为实用点，用来限定前面N个匹配常会用到*/ 
第五：nth-child(odd) 与 :nth-child(even) 
//分别匹配序号为奇数与偶数的元素。奇数(odd)与(2n+1)结果一样；偶数(even)与(2n+0)及(2n)结果一样。
```

### Cookie ###
```
 //赋值
 $.cookie('menuindex', menuid);
 //取值
 var 'menuindex'=$.cookie('menuindex');
```

### 遍历 ###
```
 $("#leftmenu li").each(function () {
     $(this).css("color","#000");
     $(this).children("img").attr("src","../../img/li.png");
```

### Ajax ###
无参
```
 $.ajax({
            type: "get", //使用get方法访问后台
            dataType: "json", //返回json格式的数据
            url: "/Menu/GetNav", //要访问的后台地址
            complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
            success: function (data) {//
                var dataObj = eval(data);
                $.each(dataObj, function (i, n) {
                    if (n.NavID == $.cookie('navindex'))
                        topnav.append("<li style='margin-top:15px;margin-left:10px;height:36px;line-height:30px;float:left;' id=" + n.NavID + " name='" + n.NavName + "' onclick= 'LoadMenu(" + n.NavID + ")'><img src='../../img/" + n.NavName + "02.png'></li>");
                    else
                        topnav.append("<li style='margin-top:15px;margin-left:10px;height:36px;line-height:30px;float:left;' id=" + n.NavID + " name='" + n.NavName + "' onclick= 'LoadMenu(" + n.NavID + ")'><img src='../../img/" + n.NavName + "01.png'></li>");
                    $.cookie('nav' + n.NavID, n.NavName);
                });
            }
        });
```
带参
```
$.ajax({
            type: "get", //使用get方法访问后台
            dataType: "json", //返回json格式的数据
            url: "/Menu/GetMenu", //要访问的后台地址
            data: "navid=" + navid, //要发送的数据
            success: function (data) {//
                var dataObj = eval(data);
                $.each(dataObj, function (i, n) {
                    leftmenu.append("<li id='" + n.MenuItemID + "' onclick=\"MenuSelect('" + n.Src + "','" + n.MenuItemID + "')\" ><div style='text-align:center; margin-left:0px;padding-left:0px;height:48px; margin-top:10px;background:url(../../img/left_1.png) no-repeat left top;font-family:Consolas;color:black;-moz-background-size:100% 100%;-webkit-background-size: 100% 100%;background-size: 100% 100%; '>" + n.MenuName + "</div></li>");
                });
                leftmenu.find("li:nth-child(" + $.cookie('menuindex') + ")").click();
            }
        });
```
### 清空子元素 ###
```
var leftmenu = $("#leftmenu");
leftmenu.empty();
```