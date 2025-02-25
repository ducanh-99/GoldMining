﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelList : MonoBehaviour
{
    public GameObject levels_container;
    public GameObject level_item_pattern;

    public int number_of_level;
    public int amount_per_page;
    public int current_level;

    public GameObject this_canvas;
    public Vector2 icon_spacing;

    public Rect container_dimensions;
    public Rect item_dimensions;
    public Rect screen_dimensions;
    
    List<Level> levels;

    // Start is called before the first frame update
    void Start()
    {
        current_level = 0;
        container_dimensions = levels_container.GetComponent<RectTransform>().rect;
        item_dimensions = level_item_pattern.GetComponent<RectTransform>().rect;
        screen_dimensions = this_canvas.GetComponent<RectTransform>().rect;

        int maxInARow = Mathf.FloorToInt(
            (container_dimensions.width + icon_spacing.x)
            / (item_dimensions.width + icon_spacing.x));

        int maxInACol = Mathf.FloorToInt(
            (container_dimensions.height + icon_spacing.y)
            / (item_dimensions.height + icon_spacing.y));

        amount_per_page = maxInACol * maxInARow;

        levels = LevelsManager.Instance.GetAllLevels();
        number_of_level = levels.Count;

        int totalPages = Mathf.CeilToInt((float)number_of_level / amount_per_page);
        LoadPanels(totalPages); 
    }

    void LoadPanels(int numberOfPanels) {
       // Debug.Log(numberOfPanels);
        GameObject panel_clone = Instantiate(levels_container) as GameObject;
        PageSwiper swiper = levels_container.AddComponent<PageSwiper>();
        swiper.totalPages = numberOfPanels;

        for (int i = 1; i <= numberOfPanels; i++) {
            GameObject panel = Instantiate(panel_clone) as GameObject;
            panel.transform.SetParent(this_canvas.transform, false);
            panel.transform.SetParent(levels_container.transform);
            panel.name = "Page " + i;
            panel.GetComponent<RectTransform>().localPosition = 
                new Vector2(screen_dimensions.width * (i - 1), 0);

            SetUpGrid(panel);
            int numberOfIcons = i == numberOfPanels ? number_of_level - current_level : amount_per_page;
            LoadIcons(numberOfIcons, panel);
        }
        Destroy(panel_clone);
    }

    void SetUpGrid(GameObject panel) {
        GridLayoutGroup grid= panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(item_dimensions.width, item_dimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = icon_spacing;
    }

    void LoadIcons(int numberOfIcons ,GameObject parentObject) {
       // Debug.Log("NumberOfIcons " + numberOfIcons);
        for (int i = 1; i <= numberOfIcons; i++) {
            Level level = levels[current_level];
            GameObject level_item = Instantiate(level_item_pattern) as GameObject;

            level_item.transform.SetParent(this_canvas.transform, false);
            level_item.transform.SetParent(parentObject.transform);
            level_item.name = "Level " + i;

            LevelButton level_button = level_item.GetComponent<LevelButton>();
            level_button.level = level;

            current_level++;
        }
    }

    void Update()
    {
        
    }
}
; 