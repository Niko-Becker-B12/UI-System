using BoothNavigator;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoothNavigatorUiTopicEntry : UiButton
{

    public UiButton addToTourButton;

    public BoothTopic topic;

    public TextMeshProUGUI topicTitle;

    BoothNavigatorManager boothNavigatorManager => FindAnyObjectByType<BoothNavigatorManager>();


    public void Awake()
    {

        base.Awake();

        Function OnAddButtonPressed = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };

        OnAddButtonPressed.functionName.AddListener(() =>
        {

            boothNavigatorManager.AddTopicToCurrentTour(topic);

        });

        addToTourButton.onClickFunctions.Add(OnAddButtonPressed);

    }

    public override void ApplySkinData()
    {
        base.ApplySkinData();

        if(topic.title.TableReference != null)
            topicTitle.text = $"{topic?.title.GetLocalizedString()}\n<sup>{topic?.description}";

    }

    public override void OnClick()
    {
        base.OnClick();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnReset()
    {
        base.OnReset();
    }

}
