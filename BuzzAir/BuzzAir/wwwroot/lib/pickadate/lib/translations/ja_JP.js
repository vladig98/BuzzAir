// @flow
// Japanese

import type { Translation } from 'pickadate/types'

const translation: Translation = {
  firstDayOfWeek: 1,
  template: 'YYYY 年 MM 月 DD 日',
  templateHookWords: {
    MMM: [
      '1月',
      '2月',
      '3月',
      '4月',
      '5月',
      '6月',
      '7月',
      '8月',
      '9月',
      '10月',
      '11月',
      '12月',
    ],
    MMMM: [
      '1月',
      '2月',
      '3月',
      '4月',
      '5月',
      '6月',
      '7月',
      '8月',
      '9月',
      '10月',
      '11月',
      '12月',
    ],
    DDD: ['日', '月', '火', '水', '木', '金', '土'],
    DDDD: [
      '日曜日',
      '月曜日',
      '火曜日',
      '水曜日',
      '木曜日',
      '金曜日',
      '土曜日',
    ],
  },
}

export default translation
