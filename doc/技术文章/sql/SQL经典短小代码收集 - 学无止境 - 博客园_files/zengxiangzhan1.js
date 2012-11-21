 $(document).ready(function(){
  $("#blogLogo").attr("src","http://www.cnblogs.com/images/cnblogs_com/zengxiangzhan/img/logo-trans.png");

            $("#btnZzk").hide();$(".btn_my_zzk").hide();
            $(".div_my_zzk").append($("<input />").attr("type","image").attr("src","http://www.cnblogs.com/images/cnblogs_com/zengxiangzhan/img/spacer.gif").click(function(){                zzk_go();
            }));
       
 

  
  var divbottom="<div class=box-bottom></div>";
  $(".mySearch").append(divbottom);
   $(".catListPostCategory").append(divbottom);
    $(".catListPostArchive").append(divbottom);
	 $(".catListEssay").append(divbottom);
	  $(".catListBlogRank").append(divbottom);
	   $(".catListComment").append(divbottom);
	     $(".catListView").append(divbottom);
		   $(".catListFeedback").append(divbottom);
		   
   });



