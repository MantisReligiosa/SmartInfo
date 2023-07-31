<script setup lang="ts">
import {computed, defineProps} from "vue";
import {IBlockFont} from "@/interfaces/Blocks";
import * as constants from "@/constants";
import {mainStore} from "@/store/mainStore";

const props = defineProps(['block', 'scale', 'selected'])
const mStore = mainStore()

const left = computed(() => props.block.x * props.scale + 'px')
const top = computed(() => props.block.y * props.scale + 'px')
const height = computed(() => props.block.h * props.scale + 'px')
const width = computed(() => props.block.w * props.scale + 'px')
const z = computed(() => props.block.z)

const blockFont = computed(() => props.block as IBlockFont)
const color = computed(() => blockFont.value?.fontColor)
const backColor = computed(() => blockFont.value?.backColor)
const fontSize = computed(() => props.block.fontSize * props.scale + 'px')
const font = computed(() => mStore.fontNames.find( p=>p.id== props.block.fontId)?.name)
const isItalic = computed(() => blockFont.value?.formatting?.includes(constants.formatting.italic))
const isBold = computed(() => blockFont.value?.formatting?.includes(constants.formatting.bold))
const isUnderline = computed(() => blockFont.value?.formatting?.includes(constants.formatting.underlined))
const hAlign = computed(() => blockFont.value?.hAlign === constants.hAlign.left ? 'start' :
    blockFont.value?.hAlign === constants.hAlign.center ? 'center' : 'right')

</script>

<template>
  <div
      class="coordinatesStyle pointer"
      :class="{fontStyle:blockFont, italicStyle:isItalic, boldStyle:isBold, underlineStyle:isUnderline, selectedStyle:props.selected}"
  >
    <slot></slot>
  </div>
</template>

<style scoped>
@font-face {
font-family: "Roboto";
src:url("~@/content/Roboto.ttf");
}

.pointer {
  cursor: default;
}

.coordinatesStyle {
  left: v-bind(left);
  top: v-bind(top);
  height: v-bind(height);
  width: v-bind(width);
  z-index: v-bind(z);
  position: absolute;
}

.fontStyle {
  color: v-bind(color);
  background-color: v-bind(backColor);
  font-size: v-bind(fontSize);
  font-family: v-bind(font);
  text-align: v-bind(hAlign);
}

.italicStyle {
  font-style: italic;
}

.boldStyle {
  font-weight: bold;
}

.underlineStyle {
  text-decoration: underline;
}

.selectedStyle {
  border: 2px black solid;
}

</style>