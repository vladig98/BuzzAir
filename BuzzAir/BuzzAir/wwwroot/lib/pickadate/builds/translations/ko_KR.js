/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.ko_KR = factory()));
}(this, function () { 'use strict';

  // Korean
  var translation = {
    firstDayOfWeek: 1,
    template: 'YYYY 년 MM 월 DD 일',
    templateHookWords: {
      MMM: ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월'],
      MMMM: ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월'],
      DDD: ['일', '월', '화', '수', '목', '금', '토'],
      DDDD: ['일요일', '월요일', '화요일', '수요일', '목요일', '금요일', '토요일']
    }
  };

  return translation;

}));
