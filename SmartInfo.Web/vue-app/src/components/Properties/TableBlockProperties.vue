<template>
    <v-card
    title="Заголовок">
        <font-properties-new
            :font="headerFont"
            @change="onHeaderChange"
        ></font-properties-new>
    </v-card>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {IBlockFont, ITableBlock} from "@/interfaces/Blocks";
import FontPropertiesNew from "@/components/Properties/Common/FontPropertiesNew.vue";

const dStore = deviceStore()
const block = computed(() => dStore.block as ITableBlock)
const headerFont = computed(() => block.value.headerDetails)

const onHeaderChange = function (font: IBlockFont) {
  console.log(font)
  //block.value.headerDetails = font
}
</script>