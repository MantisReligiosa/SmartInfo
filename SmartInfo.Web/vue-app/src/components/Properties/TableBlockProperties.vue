<template>
  <v-container class="pa-1">
    <v-expansion-panels>
      <v-expansion-panel
          title="Заголовок"
      >
        <v-expansion-panel-text >
          <FontProperties
              class="pa-0"
              :font="headerFont"
              @change="onHeaderChange"
          ></FontProperties>
        </v-expansion-panel-text>
      </v-expansion-panel>

      <v-expansion-panel
          title="Нечетные строки"
      >
        <v-expansion-panel-text >
          <FontProperties
              class="pa-0"
              :font="oddFont"
          ></FontProperties>
        </v-expansion-panel-text>
      </v-expansion-panel>

      <v-expansion-panel
          title="Четные строки"
      >
        <v-expansion-panel-text >
          <FontProperties
              class="pa-0"
              :font="evenFont"
          ></FontProperties>
        </v-expansion-panel-text>
      </v-expansion-panel>

    </v-expansion-panels>
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