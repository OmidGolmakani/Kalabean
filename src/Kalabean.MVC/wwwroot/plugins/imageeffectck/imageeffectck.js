/**
 * copyright	Copyright (C) 2015 Cedric KEIFLIN alias ced1870
 * http://www.joomlack.fr
 * http://www.template-creator.com
 * license		GNU/GPL
 * Image Effect CK
 **/

(function($) {
	$.fn.ImageEffectck = function(options) {
		var defaults = {
			// duration : 1000				// duration of the image effect
		};
		var options = $.extend(defaults, options);
		// var image.wrap;
		$("img[class^='effectck'], img.fadeck, img.edafck, img.puffck").each(function(i, image) {
			// do only if the image is not yet wrapped
			if (! $(image).parent().is('figure')) {

			var w = $(image).attr('width');
			var fl = $(image).css('float');

			$(image).wrap('<figure class="imageeffectck ' + $(image).attr('class') + '" />');
			image.wrap = $(image).parent();

			var props = Array('border-top-left-radius'
			, 'border-top-right-radius'
			, 'border-bottom-left-radius'
			, 'border-bottom-right-radius'
			, 'float'
			);

			for (i=0;i<props.length;i++) {
				catchCssProperty(image, props[i]);
			}

			// fix for Chrome
			if (parseInt(w) > 0) {
				image.wrap.width(w).css('max-width', '100%');
			}
			// if (fl) image.wrap.css('float', fl);

			// get the the text from the image title
			var imgtitle, txt_title, txt_desc;
			if (imgtitle = $(image).attr('title')) {
				imgtitle = imgtitle.split('::');
				if (imgtitle.length >= 1) {
					txt_title = imgtitle[0];
				}
				if (imgtitle.length >= 2) {
					txt_desc = imgtitle[1];
				}
			}

			// remove the attribute on the image to avoid conflicts
			$(image).attr('class', '')
					.attr('width', '')
					.attr('height', '')
					.attr('title', '')
					.css('float', '');

			// create the final code
			image.wrap.append('<figcaption>'
					+'<div>'
						+ ( txt_title ? '<div class="imageeffectck_title">' + txt_title + '</div>' : '' )
						+ ( txt_desc ? '<div class="imageeffectck_desc">' + txt_desc + '</div>' : '' )
						// + ( $(image).attr('href', '') ? '<a href="' + $(image).attr('href') + '"></a>' : '' ) // il faut dï?½tecter parent ancre >> pas besoin l'ancre s'applique sur figure
					+'</div>'
					+'</figcaption>')
			}
		});

		function catchCssProperty(image, prop) {
			var v = $(image).css(prop);
			if (parseInt(v)) {
				$(image).css(prop, '');
				$(image.wrap).css(prop, v);
			}
		}
	}
})(jQuery);