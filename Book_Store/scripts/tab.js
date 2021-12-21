
$.fn.tabBox.render = function() { 
    $(".tabBox").css({ 
    width : $.fn.tabBox.defaults.width+"px", 
    height : $.fn.tabBox.defaults.height+"px", 
    position : "relative", 
    border : "1px #ccc solid", 
    background : "url("+$.fn.tabBox.defaults.basePath+"tabHead.gif) top left repeat-x" 
    }); 
    $(".tabBox h2").each(function(i){ 
    $(this).css({ 
    width : "80px", 
    height : "30px", 
    position : "absolute", 
    "border-top" : "none", 
    cursor : "pointer", 
    left : 10+(i*80), 
    background : "url("+$.fn.tabBox.defaults.basePath+"tabNormal.gif) top right no-repeat", 
    "text-align" : "center", 
    "font-size" : "12px", 
    "font-weight" : "normal", 
    color : "#06c", 
    "line-height" : "22px" 
    }); 
    }); 
    $(".tabBox div").each(function(){ 
    $(this).css({ 
    width : $.fn.tabBox.defaults.width+"px", 
    height : ($.fn.tabBox.defaults.height-30)+"px", 
    display : "none", 
    position : "absolute", 
    top : "30px" 
    }); 
    }); 
    $(".tabBox h2.curTab").css({ 
    background : "url("+$.fn.tabBox.defaults.basePath+"tabCurTab.gif) top center no-repeat", 
    "font-weight" : "bolder" 
    }); 
    $(".tabBox h2.curTab + div").css({ 
    display : "block" 
    }); 
    }; 
    