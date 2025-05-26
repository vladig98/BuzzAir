/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.ne_NP = factory()));
}(this, function () { 'use strict';

  // Nepali
  var translation = {
    firstDayOfWeek: 1,
    template: 'DDDD, DD MMMM, YYYY',
    templateHookWords: {
      MMM: ['जन', 'फेब्रु', 'मार्च', 'अप्रिल', 'मे', 'जुन', 'जुल', 'अग', 'सेप्टे', 'अक्टो', 'नोभे', 'डिसे'],
      MMMM: ['जनवरी', 'फेब्रुअरी', 'मार्च', 'अप्रिल', 'मे', 'जुन', 'जुलाई', 'अगस्त', 'सेप्टेम्बर', 'अक्टोबर', 'नोवेम्बर', 'डिसेम्बर'],
      DDD: ['सोम', 'मंगल्', 'बुध', 'बिही', 'शुक्र', 'शनि', 'आईत'],
      DDDD: ['सोमबार', 'मङ्लबार', 'बुधबार', 'बिहीबार', 'शुक्रबार', 'शनिबार', 'आईतबार']
    }
  };

  return translation;

}));
