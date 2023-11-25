<template>
    <v-card
    title="Заголовок">
        <FontProperties
            :font="headerFont"
            @change="onHeaderChange"
        ></FontProperties>
    </v-card>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {IBlockFont, ITableBlock} from "@/interfaces/Blocks";
import FontProperties from "@/components/Properties/Common/FontProperties.vue";

const dStore = deviceStore()
const block = computed(() => dStore.block as ITableBlock)
const headerFont = computed(() => block.value.headerDetails)

const onHeaderChange = function (font: IBlockFont) {
  console.log(font)
  //block.value.headerDetails = font
}
</script>