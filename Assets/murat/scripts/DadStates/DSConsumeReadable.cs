using UnityEngine;

public class DSConsumeReadable : DadState
{
    public override DadStateType Type {get {return DadStateType.CONSUME_READABLE;}}
    [SerializeField] MinMax _bookReadTime, _magazineReadTime;
    [SerializeField] GameObject _holdingBook, _holdingPaper;
    [SerializeField] DadLine[] _afterReadingMagazineLines;

    bool isBook;
    float timer;
    public override void OnStateStarted()
    {
        isBook = Dad.CurrentItem.Key == "book";
        if(isBook)
            BookManager.RemovedLastBook();
        timer = isBook ? _bookReadTime.GetRandom() : _magazineReadTime.GetRandom(); 
        _holdingBook.SetActive(isBook);
        _holdingPaper.SetActive(!isBook);
    }

    public override void OnStateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            _holdingPaper.SetActive(false);
            _holdingBook.SetActive(false);
            if(isBook)
            {
                dad.ChangeState(DadStateType.BE_MORE_TOLERANT);
            }
            else
            {
                DadNotification.Show(DadLine.GetOptimalLine(_afterReadingMagazineLines));
                Dad.CurrentItem.OnConsumptionFinish();
                dad.ChangeState(DadStateType.WAIT);
            }
        }
    }
}
