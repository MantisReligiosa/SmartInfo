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

</script>

<style scoped>

</style>