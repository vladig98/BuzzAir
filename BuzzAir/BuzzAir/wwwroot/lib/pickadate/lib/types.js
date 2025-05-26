// @flow

//////////////
// GENERICS //
//////////////

export type WeekdaysIndex = 0 | 1 | 2 | 3 | 4 | 5 | 6
export type Weekdays = [string, string, string, string, string, string, string]
export type Months = [
  string,
  string,
  string,
  string,
  string,
  string,
  string,
  string,
  string,
  string,
  string,
  string,
]
export type Hour =
  | 0
  | 1
  | 2
  | 3
  | 4
  | 5
  | 6
  | 7
  | 8
  | 9
  | 10
  | 11
  | 12
  | 13
  | 14
  | 15
  | 16
  | 17
  | 18
  | 19
  | 20
  | 21
  | 22
  | 23
export type Minute =
  | 0
  | 1
  | 2
  | 3
  | 4
  | 5
  | 6
  | 7
  | 8
  | 9
  | 10
  | 11
  | 12
  | 13
  | 14
  | 15
  | 16
  | 17
  | 18
  | 19
  | 20
  | 21
  | 22
  | 23
  | 24
  | 25
  | 26
  | 27
  | 28
  | 29
  | 30
  | 31
  | 32
  | 33
  | 34
  | 35
  | 36
  | 37
  | 38
  | 39
  | 40
  | 41
  | 42
  | 43
  | 44
  | 45
  | 46
  | 47
  | 48
  | 49
  | 50
  | 51
  | 52
  | 53
  | 54
  | 55
  | 56
  | 57
  | 58
  | 59

///////////
// STORE //
///////////

export type Store<State> = {
  addActor: (Actor<State, any>) => any,
  subscribe: (listener: Function) => Function,
  getState: () => State,
}

export type Actor<State, Payload> = (
  state: State,
  payload: Payload
) => ?$Shape<State>

export type Action<Payload = void, Return = void> = (payload: Payload) => Return

/////////////////
// DATE PICKER //
/////////////////

export type TemplateHookWords = {
  MMM: Months,
  MMMM: Months,
  DDD: Weekdays,
  DDDD: Weekdays,
}

export type DatePickerState = {|
  disabled: Array<WeekdaysIndex | Date | [Date, Date]>,
  firstDayOfWeek: WeekdaysIndex,
  highlighted: Date,
  maximum: null | Date,
  minimum: null | Date,
  selected: null | Date,
  template: string,
  templateHookWords: TemplateHookWords,
  view: Date,
|}
export type DatePickerPartialState = $Shape<{
  ...$Diff<DatePickerState, { templateHookWords: any }>,
  templateHookWords: $Shape<TemplateHookWords>,
}>

export type DatePickerActor<Payload = void> = Actor<DatePickerState, Payload>

export type SetStatePayload = {
  ...$Diff<DatePickerPartialState, { highlighted: any }>,
}
export type SetSelectedPayload = {
  value: Date | string | number,
  isUpdatingView?: boolean,
}
export type SetViewPayload = {
  value: Date,
}
export type SetHighlightedPayload = {
  keyCode: number,
}
export type SetDisabledPayload = {
  value: $ElementType<DatePickerState, 'disabled'>,
}
export type SetMinimumPayload = {
  value: Date,
}
export type SetMaximumPayload = {
  value: Date,
}
export type SetFirstDayOfWeekPayload = {
  value: WeekdaysIndex,
}
export type SetTimePayload = $Shape<{
  hours: Hour,
  minutes: number,
}>
export type SetTemplatePayload = {
  value: string,
}
export type SetTemplateHookWordsPayload = $Shape<TemplateHookWords>

export type DatePickerCoreApi = {
  store: Store<DatePickerState>,
  getValue: () => string,
  subscribeToSelection: (
    listener: ({ value: string, date: Date | null }) => void
  ) => Function,
  setState: Action<SetStatePayload>,
  setSelected: Action<SetSelectedPayload>,
  setHighlighted: Action<SetHighlightedPayload>,
  setDisabled: Action<SetDisabledPayload>,
  setMinimum: Action<SetMinimumPayload>,
  setMaximum: Action<SetMaximumPayload>,
  setTime: Action<SetTimePayload>,
  setTemplate: Action<SetTemplatePayload>,
  setTemplateHookWords: Action<SetTemplateHookWordsPayload>,
  setFirstDayOfWeek: Action<SetFirstDayOfWeekPayload>,
  clear: Action<>,
  viewPrevious: Action<>,
  viewNext: Action<>,
  viewToday: Action<>,
  cycleHourUp: Action<>,
  cycleHourDown: Action<>,
  cycleMinuteUp: Action<>,
  cycleMinuteDown: Action<>,
  cycleMeridiem: Action<>,
}

/////////////////
// TRANSLATION //
/////////////////

export type Translation = {
  firstDayOfWeek: $ElementType<DatePickerState, 'firstDayOfWeek'>,
  template: $ElementType<DatePickerState, 'template'>,
  templateHookWords: $ElementType<DatePickerState, 'templateHookWords'>,
}

export type TranslationShape = $Shape<{
  firstDayOfWeek: $ElementType<DatePickerState, 'firstDayOfWeek'>,
  template: $ElementType<DatePickerState, 'template'>,
  templateHookWords: $Shape<$ElementType<DatePickerState, 'templateHookWords'>>,
}>

//////////
// MODE //
//////////

export type Mode = 'date' | 'date-time'
