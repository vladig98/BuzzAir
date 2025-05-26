/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.km_KH = factory()));
}(this, function () { 'use strict';

  // Khmer
  var translation = {
    firstDayOfWeek: 1,
    template: 'ថ្ងៃទី D ខែMMMM ឆ្នាំ YYYY',
    templateHookWords: {
      MMM: ['មក.', 'កុ.', 'មី.', 'មេ.', 'ឧស.', 'មិថុ.', 'កក្ក.', 'សី.', 'កញ.', 'តុ.', 'វិច្ឆ.', 'ធ.'],
      MMMM: ['មករា', 'កុម្ភៈ', 'មីនា', 'មេសា', 'ឧសភា', 'មិថុនា', 'កក្កដា', 'សីហា', 'កញ្ញា', 'តុលា', 'វិច្ឆិកា', 'ធ្នូ'],
      DDD: ['អា.', 'ច.', 'អ.', 'ព.', 'ព្រ.', 'សុ.', 'ស.'],
      DDDD: ['អាទិត្យ', 'ចន្ទ', 'អង្គារ', 'ពុធ', 'ព្រហស្បតិ៍', 'សុក្រ', 'សៅរ៍']
    }
  };

  return translation;

}));
