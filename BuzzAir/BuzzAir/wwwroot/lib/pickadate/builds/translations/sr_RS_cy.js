/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.sr_RS_cy = factory()));
}(this, function () { 'use strict';

  // Serbian (Cyrillic)
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY.',
    templateHookWords: {
      MMM: ['јан.', 'феб.', 'март', 'апр.', 'мај', 'јун', 'јул', 'авг.', 'септ.', 'окт.', 'нов.', 'дец.'],
      MMMM: ['јануар', 'фебруар', 'март', 'април', 'мај', 'јун', 'јул', 'август', 'септембар', 'октобар', 'новембар', 'децембар'],
      DDD: ['нед.', 'пон.', 'ут.', 'ср.', 'чет.', 'пет.', 'суб.'],
      DDDD: ['недеља', 'понедељак', 'уторак', 'среда', 'четвртак', 'петак', 'субота']
    }
  };

  return translation;

}));
