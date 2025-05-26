/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.hi_IN = factory()));
}(this, function () { 'use strict';

  // Hindi
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD/MM/YYYY',
    templateHookWords: {
      MMM: ['जन', 'फर', 'मार्च', 'अप्रैल', 'मई', 'जून', 'जु', 'अग', 'सित', 'अक्टू', 'नव', 'दिस'],
      MMMM: ['जनवरी', 'फरवरी', 'मार्च', 'अप्रैल', 'मई', 'जून', 'जुलाई', 'अगस्त', 'सितम्बर', 'अक्टूबर', 'नवम्बर', 'दिसम्बर'],
      DDD: ['रवि', 'सोम', 'मंगल', 'बुध', 'गुरु', 'शुक्र', 'शनि'],
      DDDD: ['रविवार', 'सोमवार', 'मंगलवार', 'बुधवार', 'गुरुवार', 'शुक्रवार', 'शनिवार']
    }
  };

  return translation;

}));
