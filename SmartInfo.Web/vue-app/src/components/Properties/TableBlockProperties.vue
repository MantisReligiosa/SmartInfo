<template>
  <v-container class="pa-1">
    <v-card>
      <label>Заголовок</label>
      <FontProperties
          :font="headerFont"
          @change="onHeaderChange"
      ></FontProperties>
    </v-card>
    <v-card>
      <label>Нечетные строки</label>
      <FontProperties
          :font="oddFont"
      ></FontProperties>
    </v-card>
    <v-card>
      <label>Четные строки</label>
      <FontProperties
          :font="evenFont"
      ></FontProperties>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {IBlockFont, ITableBlock} from "@/interfaces/Blocks";
import FontProperties from "@/components/Properties/Common/FontProperties.vue";

const dStore = deviceStore()
const block = computed(() => dStore.block as ITableBlock)
const headerFont = computed(() => block.value.headerDetails)
const oddFont = computed(() => block.value.oddRowsDetails)
const evenFont = computed(() => block.value.evenRowsDetails)

const onHeaderChange = function (font: IBlockFont) {
  console.log(font)
  //block.value.headerDetails = font
}
</script>