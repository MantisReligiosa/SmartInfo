<template>
  <v-select
      :items="formats"
      item-value="id"
      :model-value="block.formatId"
      @update:modelValue="setFormatId"
      density="compact"
      variant="underlined"
      class="mx-2"
  ></v-select>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {IDatetimeBlock} from "@/interfaces/Blocks";
import {mainStore} from "@/store/mainStore";

const dStore = deviceStore()
const mStore = mainStore()
const block = computed(() => dStore.block as IDatetimeBlock)

const formats = computed(() => mStore.dateTimeFormats.map(f => {
      return {id: f.Value, title: f.Title}
    })
)

const setFormatId = (id: number) => {
  if (!block.value)
    return
  block.value.formatId = id
}

</script>

<style scoped>

</style>