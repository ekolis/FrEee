﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Messages;

namespace FrEee.Gameplay.Commands.Messages;

/// <summary>
/// Creates commands allowing players to manage diplomatic messages.
/// </summary>
public interface IMessageCommandService
{
    IDeleteMessageCommand DeleteMessage(IMessage message);

    ISendMessageCommand SendMessage(IMessage message);
}