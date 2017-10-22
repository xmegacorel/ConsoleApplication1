module CruiseControl

type HorseInfo = 
    {
        Distanse: double
        Speed: double
        DepartureTime: double }

type Solver() =

    let getHorseInfo distanse speed fullPath = 
        let dpt = (fullPath - distanse) / speed
        let p1 =
            {
                Distanse = distanse
                Speed = speed
                DepartureTime = dpt
            }
        p1

    let initData overallDistanse =
        [ 
            getHorseInfo 120.0 60.0 overallDistanse
            getHorseInfo 60.0 90.0 overallDistanse
        ]

    let calcMeetingTime info1 info2 =
        let diff = Math.Abs(info1.Speed - info2.Speed) < 0.0000001
        if diff then
            0.0
        else   
            (info2.Distanse - info1.Distanse) / (info1.Speed - info2.Speed)

    let solveInner infos overallDistanse = 
        let listInfo = Array.sortBy (fun (x : HorseInfo) -> x.Distanse) infos
        let mutable indexOne = 0; let mutable indexTwo = 1; let mutable currentSpeed = 1000000000.0
        let mutable continueLooping = true
        while continueLooping do
            let info1 = listInfo.[indexOne]
            let info2 = listInfo.[indexTwo]
            let t = calcMeetingTime info1 info2
            match t with
            | t when t <= 0.0 ->
                let overalTime = (overallDistanse - info1.Distanse) / info1.Speed
                currentSpeed <- overallDistanse / overalTime
                indexTwo <- indexTwo + 1
                if indexTwo >= listInfo.Length then 
                    continueLooping <- false
            | (t) -> 
                    let distanseToFinish = overallDistanse - info2.Distanse - info2.Speed * t
                    let overalTime = distanseToFinish / info2.Speed + t
                    currentSpeed <- overallDistanse / overalTime
                    indexOne <- indexOne + 1
                    indexTwo <- indexTwo + 1
                    if indexTwo >= listInfo.Length then
                        continueLooping <- false
        currentSpeed

    member this.Do() =
        let overallDistanse = 300.0
        let data = initData overallDistanse
        let result = solveInner data overallDistanse
        result    

[<EntryPoint>]
let main argv =
    let solver = new Solver()
    printf "%f" solver.Do
    0 // return an integer exit code        