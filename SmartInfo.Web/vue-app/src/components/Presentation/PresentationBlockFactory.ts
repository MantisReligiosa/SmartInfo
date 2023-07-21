import {IBlock} from "@/interfaces/Blocks";
import * as Constants from "@/constants";
import TextBlock from "@/components/Presentation/TextBlock.vue";
import ScenarioBlock from "@/components/Presentation/ScenarioBlock.vue";
import DataBlock from "@/components/Presentation/DataBlock.vue";
import PictureBlock from "@/components/Presentation/PictureBlock.vue";
import TableBlock from "@/components/Presentation/TableBlock.vue";

export function BuildPresentationBlock(b: IBlock) {
    switch (b.type) {
        case Constants.blockType.text:
            return TextBlock
        case Constants.blockType.dateTime:
            return DataBlock
        case Constants.blockType.picture:
            return PictureBlock
        case Constants.blockType.table:
            return TableBlock
        case Constants.blockType.scenario:
            return ScenarioBlock
    }
}