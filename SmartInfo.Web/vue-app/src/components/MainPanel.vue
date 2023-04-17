<template>
    <v-app-bar
            color="primary"
    >
        <v-menu
                open-on-hover
        >
            <template
                    v-slot:activator="{ props }"
            >
                <v-app-bar-nav-icon
                        variant="text"
                        v-bind="props"
                ></v-app-bar-nav-icon>
            </template>
            <v-list>
                <v-list
                        :items="mainMenu"
                ></v-list>
            </v-list>
        </v-menu>
        <v-item-group
                selected-class="bg-blue"
                mandatory
        >
            <v-item
                    v-slot="{ selectedClass, toggle }"
            >
                <v-btn
                        size="x-large"
                        :class="[selectedClass, asDefault?'bg-blue':'']"
                        @click="switchToSelectionMode(toggle)"
                >
                    <v-icon
                            icon="mdi-navigation-variant-outline"
                            class="mdi-flip-h"
                            size="x-large"
                    ></v-icon>
                    <v-tooltip
                            activator="parent"
                            location="bottom"
                    >{{ tooltipSelect }}
                    </v-tooltip>
                </v-btn>
            </v-item>
            <v-item
                    v-slot="{ selectedClass, toggle }"
            >
                <v-menu
                        open-on-hover
                >
                    <template
                            v-slot:activator="{ props }"
                    >
                        <v-btn
                                size="x-large"
                                v-bind="props"
                                :class="selectedClass"
                        >
                            <v-icon
                                    :icon="blockMenuIcon"
                                    size="x-large"
                            ></v-icon>
                            <v-icon
                                    icon="mdi-chevron-down"
                            ></v-icon>
                            <v-tooltip
                                    activator="parent"
                                    location="bottom"
                            >{{ tooltipBlocks }}
                            </v-tooltip>
                        </v-btn>
                    </template>
                    <v-list>
                        <v-list-item
                                v-for="b in blocksMenu"
                                :key="b"
                                @click="switchToDrawMode(b, toggle)"
                        >
                            <v-icon
                                    class="mr-2"
                                    :icon="b.props.prependIcon"
                            ></v-icon>
                            <v-label>
                                {{ b.title }}
                            </v-label>
                        </v-list-item>
                    </v-list>
                </v-menu>
            </v-item>
        </v-item-group>
        <v-spacer></v-spacer>
        <v-menu
                open-on-hover
        >
            <template
                    v-slot:activator="{ props }"
            >
                <v-btn
                        size="x-large"
                        v-bind="props"
                >
                    {{ selectedDevice?.name || '<...>' }}
                    <v-icon
                            icon="mdi-chevron-down"
                    ></v-icon>
                </v-btn>
            </template>
            <v-list>
                <v-list-item
                        v-for="(device, index) in devices"
                        @click="selectDeviceById(device.id)"
                        :title="device.name"
                        :key="index"
                ></v-list-item>
            </v-list>
        </v-menu>
        <v-btn
                icon="mdi-upload"
                :disabled="!edit.hasChanges"
                :variant="edit.variant"
        ></v-btn>
        <v-spacer></v-spacer>
        <v-btn
                variant="plain"
        >
            <v-icon
                    size="x-large"
                    icon="mdi-play"
            ></v-icon>
            <v-tooltip
                    activator="parent"
                    location="bottom"
            >{{ tooltipPlay }}
            </v-tooltip>
        </v-btn>
        <v-btn
                variant="plain"
        >
            <v-icon
                    size="x-large"
                    icon="mdi-stop"
            ></v-icon>
            <v-tooltip
                    activator="parent"
                    location="bottom"
            >{{ tooltipStop }}
            </v-tooltip>
        </v-btn>
        <v-btn
                variant="plain"
        >
            <v-icon
                    size="x-large"
                    icon="mdi-refresh"
            ></v-icon>
            <v-tooltip
                    activator="parent"
                    location="bottom"
            >{{ tooltipRefresh }}
            </v-tooltip>
        </v-btn>
    </v-app-bar>
</template>

<script setup lang="ts">
import * as Constants from '@/constants'
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import {computed, ref} from "vue";

interface blocksMenuItem {
    title: string,
    value: Constants.blockType,
    props: { prependIcon: string }
}

const mStore = mainStore()
const dStore = deviceStore()
const tooltipSelect = ref('Выбор')
const tooltipBlocks = ref('Блоки')
const tooltipPlay = ref('Пуск')
const tooltipStop = ref('Стоп')
const tooltipRefresh = ref('Перезагрузить')
const mainMenu = ref([
    {
        title: 'Item #1',
        value: 1,
    },
    {
        title: 'Item #2',
        value: 2,
    },
    {
        title: 'Item #3',
        value: 3,
    },
])
const blockMenuIcon = ref<string>(Constants.blockIcon.text)

const textItem:blocksMenuItem={
    title: 'Текст',
    value: Constants.blockType.text,
    props: {
        prependIcon: Constants.blockIcon.text,
    },
}

const blocksMenu = ref([
    textItem,
    {
        title: 'Таблица',
        value: Constants.blockType.table,
        props: {
            prependIcon: Constants.blockIcon.table,
        },
    },
    {
        title: 'Изображение',
        value: Constants.blockType.picture,
        props: {
            prependIcon: Constants.blockIcon.picture,
        },
    },
    {
        title: 'ДатаВремя',
        value: Constants.blockType.dateTime,
        props: {
            prependIcon: Constants.blockIcon.dateTime,
        },
    },
    {
        title: 'Сценарий',
        value: Constants.blockType.scenario,
        props: {
            prependIcon: Constants.blockIcon.scenario,
        },
    },
])
const asDefault = ref(true)
const selectDeviceById = async (deviceId: number) => {
    if (edit.value.hasChanges) {
        let ok = await confirm('Изменения не сохранены. Продолжить?');
        if (!ok) {
            return;
        }
    }
    mainStore().selectDeviceById(deviceId)
    await deviceStore().loadById(deviceId)
}

const switchToSelectionMode = (toggle: any) => {
    asDefault.value = false
    mStore.switchToSelectionMode()
    toggle()
}
const switchToDrawMode = (item:blocksMenuItem, toggle: any) => {
    blockMenuIcon.value = item.props.prependIcon
    asDefault.value = false
    mStore.switchToDrawMode(item.value)
    toggle()
}
const devices = computed(() => mStore.devices)
const selectedDevice = computed(() => mStore.device)
const edit = computed(() => {
    return {hasChanges: dStore.edited, variant: dStore.edited ? 'tonal' : 'plain'}
})
</script>

<style scoped>

</style>