export const blockType = Object.freeze({
    text: 10,
    dateTime: 20,
    picture: 30,
    table: 40,
    scenario: 50
})

export const blockIcon = Object.freeze({
    text: 'mdi-format-text',
    dateTime: 'mdi-calendar-clock-outline',
    picture: 'mdi-image',
    table: 'mdi-table',
    scenario: 'mdi-movie-open-outline'
})

export const mode = Object.freeze({
    selection: 1,
    drawing: 2
})

export const hAlign = Object.freeze({
    left:0,
    center:1,
    right:2
})

export const vAlign = Object.freeze({
    top:0,
    center:1,
    bottom:2
})

export const formatting = Object.freeze({
    italic:0,
    bold:1,
    underlined:2
})
