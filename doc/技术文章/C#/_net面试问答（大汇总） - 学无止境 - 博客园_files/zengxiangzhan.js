
Site = {
    
    initialize: function() {
        Site.animSidebar();
    },
    
    animSidebar: function() {
        var anchors = $$(".catListPostCategory a", ".catListPostArchive a", ".catListEssay a", ".catListView a", ".catListFeedback a");
        anchors.each(function(el) {
            var fx = new Fx.Morph(el, { duration: 618, wait: false });
            // var fx = new Fx.Styles(el, { duration: 200, wait: false });
            el.addEvent("mouseenter", function() {
                fx.start({
                    "margin-left": 15,
					"opacity": 80,
                    "color": "#a0c100"
                });
            });
            el.addEvent("mouseleave", function() {
                fx.start({
                    "margin-left": 0,					
                    "color": "#666"
                });
            });
        });
    }
    
}

window.addEvent("domready", Site.initialize);

