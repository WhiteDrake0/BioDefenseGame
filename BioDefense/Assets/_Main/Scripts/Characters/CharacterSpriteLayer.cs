using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CHARACTERS
{
    public class CharacterSpriteLayer
    {
        private CharacterManager characterManager => CharacterManager.instance;

        private const float DEFAULT_TRANSITION_SPEED = 3f;
        private float transitionSpeedMultiplier = 1f;
        public int layer { get; private set; } = 0;
        public Image renderer { get; private set; } = null;
        public CanvasGroup rendererCG => renderer.GetComponent<CanvasGroup>();

        private List<CanvasGroup> oldRenderers = new List<CanvasGroup>();

        private Coroutine co_transitioningLayer = null;
        private Coroutine co_levelingAlphas = null;
        private Coroutine co_changingColor = null;
        public bool isTransitioningLayer => co_transitioningLayer != null;
        public bool isLevelingAlphas => co_levelingAlphas != null;
        public bool isChangingColor => co_changingColor != null;

        public CharacterSpriteLayer(Image defaultRenderer, int layer = 0)
        {
            renderer = defaultRenderer;
            this.layer = layer;

        }

        public void SetSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        public Coroutine TransitionSprite (Sprite sprite, float speed = 1)
        {
            if (sprite == renderer.sprite)
                return null;

            if (isTransitioningLayer)
                characterManager.StopCoroutine(co_transitioningLayer);

            co_transitioningLayer = characterManager.StartCoroutine(TransitioningSprite(sprite, speed));

            return co_transitioningLayer;
        }

        private IEnumerator TransitioningSprite(Sprite sprite, float speedMultiplier)
        {
            transitionSpeedMultiplier = speedMultiplier;
            Image newRenderer = CreateRenderer(renderer.transform.parent);
            newRenderer.sprite = sprite;

            yield return TryStartLevelingAlphas();

            co_transitioningLayer = null;
            /*while (isLevelingAlphas)
                yield return null;*/
        }

        private Image CreateRenderer(Transform parent)
        {
            Image newRenderer = Object.Instantiate(renderer, parent);
            oldRenderers.Add(rendererCG);

            newRenderer.name = renderer.name;
            renderer = newRenderer;
            renderer.gameObject.SetActive(true);
            rendererCG.alpha = 0;

            return newRenderer;

        }

        private Coroutine TryStartLevelingAlphas()
        {
            if (isLevelingAlphas)
                return co_levelingAlphas;

            co_levelingAlphas = characterManager.StartCoroutine(RunAlphaLeveling());

            return co_levelingAlphas;
        }

        private IEnumerator RunAlphaLeveling()
        {
            while(rendererCG.alpha < 1 || oldRenderers.Any(oldCG => oldCG.alpha > 0)) 
            {
                float speed = DEFAULT_TRANSITION_SPEED * transitionSpeedMultiplier * Time.deltaTime;
                rendererCG.alpha = Mathf.MoveTowards(rendererCG.alpha, 1, speed);

                for(int i = oldRenderers.Count - 1; i >= 0; i--)
                {
                    CanvasGroup oldCG = oldRenderers[i];
                    oldCG.alpha = Mathf.MoveTowards(oldCG.alpha, 0, speed);

                    if(oldCG.alpha <= 0)
                    {
                        oldRenderers.RemoveAt(i);
                        Object.Destroy(oldCG.gameObject);
                    }
                }

                yield return null;
            }

            co_levelingAlphas = null;
        }

        public void SetColor(Color color)
        {
            renderer.color = color;

            foreach(CanvasGroup oldCg in oldRenderers)
            {
                oldCg.GetComponent<Image>().color = color;
            }
        }

        public Coroutine TransitionColor(Color color, float speed)
        {
            if (isChangingColor)
                characterManager.StopCoroutine(co_changingColor);

            co_changingColor = characterManager.StartCoroutine(ChangingColor(color, speed));

            return co_changingColor;
        }

        public void StopChangingColor()
        {
            if (!isChangingColor)
                return;

            characterManager.StopCoroutine(co_changingColor);

            co_changingColor = null;
        }
        private IEnumerator ChangingColor(Color color, float speedMultiplier)
        {
            Color oldColor = renderer.color;
            List<Image> oldImages = new List<Image>();

            foreach(var oldCG in oldRenderers)
            {
                oldImages.Add(oldCG.GetComponent<Image>());
            }

            float colorPercent = 0;
            while(colorPercent < 1)
            {
                colorPercent += DEFAULT_TRANSITION_SPEED * speedMultiplier * Time.deltaTime;

                renderer.color = Color.Lerp(oldColor, color, colorPercent);

                foreach(Image oldImage in oldImages)
                {
                    oldImage.color = renderer.color;
                }

                yield return null;
            }

            co_changingColor = null;
        }
    }
}
