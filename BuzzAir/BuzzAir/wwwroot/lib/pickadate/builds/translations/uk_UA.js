/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.uk_UA = factory()));
}(this, function () { 'use strict';

  // Ukrainian
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD MMMM YYYY p.',
    templateHookWords: {
      MMM: ['січ', 'лют', 'бер', 'кві', 'тра', 'чер', 'лип', 'сер', 'вер', 'жов', 'лис', 'гру'],
      MMMM: ['січень', 'лютий', 'березень', 'квітень', 'травень', 'червень', 'липень', 'серпень', 'вересень', 'жовтень', 'листопад', 'грудень'],
      DDD: ['нд', 'пн', 'вт', 'ср', 'чт', 'пт', 'сб'],
      DDDD: ['неділя', 'понеділок', 'вівторок', 'середа', 'четвер', 'п‘ятниця', 'субота']
    }
  };

  return translation;

}));
