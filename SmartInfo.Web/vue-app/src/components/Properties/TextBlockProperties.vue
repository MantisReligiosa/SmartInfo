<template>
  <v-container>
      <v-text-field
              variant="underlined"
              v-model="block.text"
      ></v-text-field>
  </v-container>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {ITextBlock} from "@/interfaces/Blocks";

const dStore = deviceStore()
const block = computed(() => dStore.block as ITextBlock)

function setText(value:string) {
    if (!block.value)
        return
    block.value.text = value
}
</script>

<style scoped>

</style>