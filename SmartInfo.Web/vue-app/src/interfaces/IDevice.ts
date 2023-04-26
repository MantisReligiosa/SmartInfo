import {IBlock} from "@/interfaces/Blocks";

export interface IDevice {
    id: number
    name: string
    blocks: IBlock[],
    backColor: string
}