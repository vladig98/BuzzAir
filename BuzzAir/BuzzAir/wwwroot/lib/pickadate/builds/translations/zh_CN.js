/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.zh_CN = factory()));
}(this, function () { 'use strict';

  // Simplified Chinese
  var translation = {
    firstDayOfWeek: 1,
    template: 'YYYY 年 MM 月 DD 日',
    templateHookWords: {
      MMM: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'],
      MMMM: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
      DDD: ['日', '一', '二', '三', '四', '五', '六'],
      DDDD: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六']
    }
  };

  return translation;

}));
