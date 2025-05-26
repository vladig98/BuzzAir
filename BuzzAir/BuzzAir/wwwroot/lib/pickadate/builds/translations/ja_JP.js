/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.ja_JP = factory()));
}(this, function () { 'use strict';

  // Japanese
  var translation = {
    firstDayOfWeek: 1,
    template: 'YYYY 年 MM 月 DD 日',
    templateHookWords: {
      MMM: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
      MMMM: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
      DDD: ['日', '月', '火', '水', '木', '金', '土'],
      DDDD: ['日曜日', '月曜日', '火曜日', '水曜日', '木曜日', '金曜日', '土曜日']
    }
  };

  return translation;

}));
