/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.sk_SK = factory()));
}(this, function () { 'use strict';

  // Slovak
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY',
    templateHookWords: {
      MMM: ['jan', 'feb', 'mar', 'apr', 'máj', 'jún', 'júl', 'aug', 'sep', 'okt', 'nov', 'dec'],
      MMMM: ['január', 'február', 'marec', 'apríl', 'máj', 'jún', 'júl', 'august', 'september', 'október', 'november', 'december'],
      DDD: ['Ne', 'Po', 'Ut', 'St', 'Št', 'Pi', 'So'],
      DDDD: ['nedeľa', 'pondelok', 'utorok', 'streda', 'štvrtok', 'piatok', 'sobota']
    }
  };

  return translation;

}));
