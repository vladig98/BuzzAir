/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.ru_RU = factory()));
}(this, function () { 'use strict';

  // Russian
  var translation = {
    firstDayOfWeek: 1,
    template: 'D MMMM YYYY г.',
    templateHookWords: {
      MMM: ['янв', 'фев', 'мар', 'апр', 'май', 'июн', 'июл', 'авг', 'сен', 'окт', 'ноя', 'дек'],
      MMMM: ['января', 'февраля', 'марта', 'апреля', 'мая', 'июня', 'июля', 'августа', 'сентября', 'октября', 'ноября', 'декабря'],
      DDD: ['вс', 'пн', 'вт', 'ср', 'чт', 'пт', 'сб'],
      DDDD: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота']
    }
  };

  return translation;

}));
