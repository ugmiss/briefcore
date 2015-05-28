# wpf的内容控件 #
> 继承自`ContentControl`控件的，我们称之为内容控件.
```
<ContentControl Content=""></ContentControl> 
```


`ContentControl`控件定义了一个Content,在没有框架的情况下,也可以将其作为一个内容区域.然而为了满足ui的需求,我们还需要各种不同的控件来当内容区域,如`TabControl`,`DockPanel`,`Selector`等。有些控件则继承自`ItemsControl`属于集合控件，不属于内容控件.但他们根据不同需求，同时都可以当容器使用，但他们的使用方式却不同.
为了统一对内容区域的操作,prism提供了一种适配模式,也可以说提供了控件与Region的映射关系.将不同可以作为容器的控件的操作方式统一为Region的操作方式.

## prism内置有三种控件可以作为内容区域适配对象 ##

  * `ContentControl`
  * `ItemsControl`
  * `Selector`