export enum blockType {
    text = 10,
    dateTime = 20,
    picture = 30,
    table = 40,
    scenario = 50
}

export enum blockIcon {
    text = 'mdi-format-text',
    dateTime = 'mdi-calendar-clock-outline',
    picture = 'mdi-image',
    table = 'mdi-table',
    scenario = 'mdi-movie-open-outline'
}

export enum editorMode {
    selection = 1,
    drawing = 2
}

export enum hAlign {
    left = 0,
    center = 1,
    right = 2
}

export enum vAlign {
    top = 0,
    center = 1,
    bottom = 2
}

export enum formatting {
    italic = 0,
    bold = 1,
    underlined = 2
}
