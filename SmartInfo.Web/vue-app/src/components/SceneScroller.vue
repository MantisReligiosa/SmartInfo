<template>
    <v-card
            flat
            rounded="0"
    >
        <v-window 
            v-model="index"
        >
            <v-window-item
                    v-for="s in length"
                    :key="s"
                    :value="s"
            >
                <v-card
                        class="d-flex justify-center align-center"
                >
                    <v-label>
                        {{ captionByIndex() }}
                    </v-label>
                </v-card>
            </v-window-item>
        </v-window>

        <v-card-actions
                class="justify-space-between pa-0"
        >
            <v-btn
                    variant="plain"
                    icon="mdi-chevron-left"
                    @click.stop="prev"
            ></v-btn>
            <v-item-group
                    v-model="index"
                    class="text-center"
                    mandatory
            >
                <v-item
                        v-for="n in length"
                        :key="`btn-${n}`"
                        v-slot="{ isSelected, toggle }"
                        :value="n"
                >
                    <v-btn
                            :variant="isSelected ? 'tonal' : 'text'"
                            :color="isSelected ? 'info' : 'black'"
                            icon="mdi-record"
                            @click.stop="selectByOrder(n, toggle)"
                            size="x-small"
                            style="max-width: 15px; max-height: 15px"
                    ></v-btn>
                </v-item>
            </v-item-group>
            <v-btn
                    variant="plain"
                    icon="mdi-chevron-right"
                    @click.stop="next"
            ></v-btn>
        </v-card-actions>
    </v-card>
</template>

<script setup lang="ts">
import {defineProps, defineEmits, ref, computed} from "vue"
import {IScene} from "@/interfaces/Blocks";

interface IProps {
    scenes: IScene[]
}

const props = defineProps<IProps>()

const emits = defineEmits<{ (e: 'selectionChanged', scene: IScene): void }>()

const length = computed(() => props.scenes?.length ?? 0)
const index = ref(0)

const next = () => {
    index.value = index.value + 1 > length.value
        ? 1
        : index.value + 1
    emitByIndex(index.value)
}
const prev = () => {
    index.value = index.value - 1 <= 0
        ? length.value
        : index.value - 1
    emitByIndex(index.value)
}

const selectByOrder = (i: number, t: any) => {
    emitByIndex(i)
    t()
}

const emitByIndex = (i: number) =>
{
    const scene = props.scenes.find(s => s.order === i)
    if (scene) {
        emits('selectionChanged', scene)
    }
}

const captionByIndex = () => {
    if (index?.value == undefined) {
        return ''
    }
    const scene = props.scenes.find(s => s.order === index.value)
    return scene?.caption ?? ''
}

</script>

<style scoped>

</style>