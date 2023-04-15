<template>
    <v-container class="px-0">
        <v-text-field
                v-if="isBlockSelected"
                class="centered-input"
                density="compact"
                variant="underlined"
                v-model="block.caption"
                spellcheck="false"
        ></v-text-field>
        <v-card
                v-if="isBlockSelected"
        >
            <GeometryProperties></GeometryProperties>
        </v-card>
        <v-card
                class="mt-1"
                v-if="blockHaveText"
        >
            <FontProperties></FontProperties>
        </v-card>
    </v-container>
</template>

<script>
import {deviceStore} from '@/store/deviceStore'
import {blockType} from "@/constants";
import GeometryProperties from "@/components/Properties/Common/GeometryProperties.vue";
import FontProperties from "@/components/Properties/Common/FontProperties.vue";

export default {
    name: "BlockProperties",
    data: () => ({
        currentBlockId: 0
    }),
    computed: {
        block() {
            return deviceStore().block
        },
        isBlockSelected() {
            return deviceStore().block && deviceStore().block.type > 0
        },
        blockHaveText() {
            return deviceStore().block
                && [blockType.text, blockType.table, blockType.dateTime].includes(deviceStore().block.type);
        }
    },
    components: {
        GeometryProperties,
        FontProperties
    }
}
</script>

<style scoped>
:deep(.v-input__prepend) {
    margin-right: 0 !important;
}

:deep(.v-col) {
    padding: 0 !important;
}

.centered-input >>> input {
    text-align: center
}
</style>