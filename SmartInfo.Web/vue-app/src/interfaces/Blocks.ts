import * as Constants from "@/constants";

export interface IBlock {
    id: number
    type: Constants.blockType
    caption: string
    x: number
    y: number
    h: number
    w: number
    z: number
}

export interface IBlockFont {
    fontId: number
    fontSize: number
    fontColor: string
    backColor: string
    formatting: Constants.formatting[]
    vAlign: Constants.vAlign
    hAlign: Constants.hAlign
}

export interface ITextBlock extends IBlock, IBlockFont {
    text: string
    isAnimationEnabled: boolean
    animationStyle: number
    holdBeforeAnimation: number
    holdAfterAnimation: number
    animationSpeed: number
}

export interface IDatetimeBlock extends IBlock, IBlockFont {
    formatId: number
}

export interface ITableBlock extends IBlock, IBlockFont {

}

export interface IPictureBlock extends IBlock {

}

export interface IScenario extends IBlock {

}