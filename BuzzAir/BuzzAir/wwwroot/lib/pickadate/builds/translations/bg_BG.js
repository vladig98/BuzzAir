/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.bg_BG = factory()));
}(this, function () { 'use strict';

  // Bulgarian
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD MMMM YYYY г.',
    templateHookWords: {
      MMM: ['янр', 'фев', 'мар', 'апр', 'май', 'юни', 'юли', 'авг', 'сеп', 'окт', 'ное', 'дек'],
      MMMM: ['януари', 'февруари', 'март', 'април', 'май', 'юни', 'юли', 'август', 'септември', 'октомври', 'ноември', 'декември'],
      DDD: ['нд', 'пн', 'вт', 'ср', 'чт', 'пт', 'сб'],
      DDDD: ['неделя', 'понеделник', 'вторник', 'сряда', 'четвъртък', 'петък', 'събота']
    }
  };

  return translation;

}));
