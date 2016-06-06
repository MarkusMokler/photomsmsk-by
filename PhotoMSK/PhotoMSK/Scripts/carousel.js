(function($){
    $.fn.fancySlider = function(options) {

        var selector = $(this);
         
        var defaults = {
                img_width : 200,       //image width  
                img_height : 200,      //image width 
                anim_speed : 500,       //animation speed
                auto_slide : true,      //auto slide enable/disable
                auto_interval : 2000,   //auto slide speed
                effect : "square",
                hover_size : "full",      //half or full
                hover_speed : 200,
                hover_opacity : 0.8,
                hover_theme : "red_hover_theme_light",
                image_theme: 'red_slider_rotate1',
                mobile_img_width: 0,
                mobile_img_height: 0,
                tablet_img_width : 0,
                tablet_img_height : 0,
                overflow: 0,
                animated_hover: true
          },
          settings = $.extend({}, defaults, options);

          if(settings.mobile_img_width == 0){
              settings.mobile_img_width = settings.img_width;
          }

          if(settings.mobile_img_height == 0){
              settings.mobile_img_height = settings.img_height;
          }


          if(settings.tablet_img_width == 0){
              settings.tablet_img_width = settings.img_width;
          }

          if(settings.tablet_img_height == 0){
              settings.tablet_img_height = settings.img_height;
          }


          if( $(window).width() <= 768){
              settings.img_width = settings.tablet_img_width;
              settings.img_height = settings.tablet_img_height;  
          }

          if( $(window).width() <= 720){
              settings.img_width = settings.mobile_img_width;
              settings.img_height = settings.mobile_img_height;  
          }          


    ////////////////////////////////settings end

    var red_slider_direction = 1;
    var red_slider_play = true;
    var red_slide_end = "left";

    if(settings.overflow != 0){
      settings.hover_opacity = 1;
    }


    //cant move to the left
    
	selector.addClass("red_slider_wrap");
  selector.find("ul").addClass(settings.image_theme);
    selector.find('ul a').css("opacity", settings.hover_opacity);
    selector.find('ul a').addClass(settings.hover_theme);


    //settings.effects   ///////////////////////////////////////
    if(settings.effect == "circle"){

      var red_circle_radius = settings.img_width/2;

      selector.find('ul > li').css({
                      borderTopLeftRadius: red_circle_radius, 
                      borderTopRightRadius: red_circle_radius, 
                      borderBottomLeftRadius: red_circle_radius, 
                      borderBottomRightRadius: red_circle_radius,
                      WebkitBorderTopLeftRadius: red_circle_radius, 
                      WebkitBorderTopRightRadius: red_circle_radius, 
                      WebkitBorderBottomLeftRadius: red_circle_radius, 
                      WebkitBorderBottomRightRadius: red_circle_radius, 
                      MozBorderRadius: red_circle_radius
                    });
    }else if(settings.effect == "grayscale"){

      selector.find("ul").addClass('red_slider_grey');

    }


    ////////////////////////////////////////////////////////

    var img_width_str = settings.img_width + "px";
    var img_height_str = settings.img_height + "px";


    selector.append("<div class='cf'></div>");
    selector.find('ul > li > img').css("width", img_width_str);
    selector.find('ul > li > img').css("height", img_height_str);
    selector.find('ul > li').css("height", img_height_str);

    var red_temp = selector.find('ul > li').css("height");
    selector.find('ul > li > a').css("top", red_temp);
   selector.find('ul > li').css("height", parseInt(settings.img_height + settings.overflow) + "px");



    
    var red_col_width = selector.find('ul li').outerWidth();
    
    var red_container_width = selector.find('ul').width();

    var red_slider_left = selector.find('ul').position().left;

    var red_total_sliders =  selector.find('ul').children('li').length ;

    var red_total_width = red_col_width * red_total_sliders;


    
    
    selector.find('ul').css("width", parseInt(red_col_width * red_total_sliders) + "px" );

    var current_left = selector.find('ul').position().left;



    //click next
    selector.find('.red_slider_next').on('click', function(e){

          if (e.originalEvent === undefined) {

          }else{
            red_slider_direction = 1;
          }


          if( red_container_width - selector.find('ul').position().left <  red_total_width ){

              selector.find('ul').stop().animate({
                  "left": "-=" + red_col_width },
                   {
                    duration: settings.anim_speed,
                    step: function(now, fx){

                      selector.find('.red_slider_prev').css("opacity", 0.8);
                      selector.find('.red_slider_next').css("opacity", 0.8);
                      if( red_container_width - selector.find('ul').position().left >  red_total_width ){
                          $(this).stop().animate({"left": current_left }, "fast", function(){

                            selector.find('.red_slider_next').css("opacity", 0.5);
                            if(settings.auto_slide){
                              red_slider_direction = 0;
                            }

                          });

                          

                      }else{
                          current_left = selector.find('ul').position().left;
                      }

              } 
            });//end of animate

            }//end of if
          

    });// on click next

    

    selector.find('.red_slider_prev').on('click', function(e){
          if (e.originalEvent === undefined) {

          }else{
            red_slider_direction = 0;
          }
          
        if( selector.find('ul').position().left < 0 ){

            selector.find('ul').stop().animate({ 
              "left": "+=" + red_col_width

            },
            {
               duration: settings.anim_speed,
               step: function(now, fx){

                selector.find('.red_slider_prev').css("opacity", 0.8);
                selector.find('.red_slider_next').css("opacity", 0.8);

                if( selector.find('ul').position().left > red_slider_left ){
                  $(this).stop().animate({"left": current_left }, "fast", function(){
                      selector.find('.red_slider_prev').css("opacity", 0.5);
                      if(settings.auto_slide){
                            red_slider_direction = 1;
                          }
                  });

                          

                  }
                    current_left = selector.find('ul').position().left;
                  

               }

             });
        }
        
    });

    //touch events

    //old hover position
    var this_top = selector.find('ul').find('a').position().top;

    var hammertime = new Hammer(selector.find('ul')[0], { prevent_default: true });

    

    hammertime.on("dragright swiperight", function(e) {

      selector.find('.red_slider_prev').trigger('click');
      
      red_slider_direction = 0;
    });

    hammertime.on("dragleft swipeleft",function(e) {
      
      selector.find('.red_slider_next').trigger('click');

      red_slider_direction = 1;
    });


    hammertime.on("hold",function(e) {
      
      if(settings.hover_size == "full"){
        selector.find('ul > li > a').stop().animate({top:0}, settings.hover_speed);
      }else if(settings.hover_size == "half"){
        selector.find('ul > li > a').stop().animate({top:'50%'}, settings.hover_speed);
      }else if(settings.hover_size == "quarter"){
        selector.find('ul > li > a').stop().animate({top:'70%'}, settings.hover_speed);  
      }  
     
      
    });

    hammertime.on("release",function(e) {


      selector.find('ul > li > a').stop().animate({top:this_top}, settings.hover_speed);
     
      
    });


    var red_focus = true;


    $(window).on('blur', function(){
      red_focus = false;

    });

    if(settings.auto_slide){


      $(window).on('focus', function(){

        if(red_focus == false){

            if(settings.auto_slide){
             
                selector.find('.red_slider_prev').trigger('click');
                selector.find('.red_slider_next').trigger('click'); 

            } 

        }  

    });



        var old_interval = setInterval(function(){

            if(red_slider_play){
              if(red_slider_direction == 1){
                selector.find('.red_slider_next').trigger('click');
              }else{
                selector.find('.red_slider_prev').trigger('click');
              }

            } 
                
          }, settings.auto_interval);

        




    }//if settings.auto_slide

    selector.find('ul').hover(function() {
      red_slider_play = 0;

      


    }, function() {
      red_slider_play = 1;
      
    });




    
    
    selector.find('ul').find('a').css('top', this_top);

if(settings.animated_hover){   

    if(settings.hover_size == "full"){

      selector.find('ul span').css('vertical-align', 'middle');
      selector.find('ul').find('li').hover(function(){
        selector.find(this).find('a').stop().animate({top:0}, settings.hover_speed);
      }, function(){
        selector.find(this).find('a').stop().animate({top:this_top}, settings.hover_speed);
      });

    }else if(settings.hover_size == "half"){

      selector.find('ul').find('li').hover(function(){
        $(this).find('a').stop().animate({top:'50%'}, settings.hover_speed);
      }, function(){
        $(this).find('a').stop().animate({top:this_top}, settings.hover_speed);
      });

    }else if(settings.hover_size == "quarter"){
            selector.find('ul').find('li').hover(function(){
              $(this).find('a').stop().animate({top:'70%'}, settings.hover_speed);
            }, function(){
              $(this).find('a').stop().animate({top:this_top}, settings.hover_speed);
            });
    } 

}


           
         
    }//end of plugin
})(jQuery);//end